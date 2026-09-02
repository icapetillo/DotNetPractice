using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using CloverTest1.Services.Clover.Models;
using CloverTest1.Services;
using CloverTest1.Data;
using System.Text;

namespace CloverTest1.Services.Clover
{
    public class CloverClient : ICloverClient
    {
        private readonly HttpClient _http;
        private readonly CloverOptions _options;
        private readonly ITenantProvider _tenantProvider;
        private readonly ApplicationDbContext _db;

        public CloverClient(HttpClient http, CloverOptions options, ITenantProvider tenantProvider, ApplicationDbContext db)
        {
            _http = http;
            _options = options;
            _tenantProvider = tenantProvider;
            _db = db;
        }

        public async Task<IEnumerable<CloverItem>> GetItemsAsync()
        {
            var tenant = _tenantProvider.GetCurrentTenant();

            // Determine token to use: tenant token preferred, otherwise fallback to global option token
            string? accessToken = tenant.CloverAccessToken;
            bool usingTenantToken = true;

            if (string.IsNullOrEmpty(accessToken) || tenant.CloverTokenExpiresAt == null || tenant.CloverTokenExpiresAt <= DateTime.UtcNow)
            {
                // Try to obtain/refresh tenant token when possible
                try
                {
                    await ObtainOrRefreshTokenAsync(tenant);
                    accessToken = tenant.CloverAccessToken;
                }
                catch
                {
                    // ignore: we'll try fallback to global token
                    accessToken = tenant.CloverAccessToken;
                }
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                // fallback to global configured token (useful for quick testing)
                accessToken = _options.AccessToken;
                usingTenantToken = false;
            }

            if (string.IsNullOrEmpty(accessToken))
                throw new InvalidOperationException("No access token available for Clover API (tenant or global)");

            var request = new HttpRequestMessage(HttpMethod.Get, $"/v3/merchants/{_options.MerchantId}/items");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var resp = await _http.SendAsync(request);

            // If unauthorized and we used a tenant token, try to refresh once
            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized && usingTenantToken && !string.IsNullOrEmpty(tenant.CloverRefreshToken))
            {
                await ObtainOrRefreshTokenAsync(tenant, forceRefresh: true);
                accessToken = tenant.CloverAccessToken;
                if (string.IsNullOrEmpty(accessToken))
                    throw new InvalidOperationException("Failed to refresh tenant access token");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                resp = await _http.SendAsync(request);
            }

            resp.EnsureSuccessStatusCode();

            using var stream = await resp.Content.ReadAsStreamAsync();
            var doc = await JsonSerializer.DeserializeAsync<CloverItemsResponse>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (doc?.Elements == null) return Array.Empty<CloverItem>();

            // Normalize cent amounts to decimals
            foreach (var item in doc.Elements)
            {
                if (item.PriceInCents.HasValue)
                {
                    item.Price = item.PriceInCents.Value / 100m;
                }
                if (item.Modifiers != null)
                {
                    foreach (var mod in item.Modifiers)
                    {
                        if (mod.PriceInCents.HasValue)
                            mod.Price = mod.PriceInCents.Value / 100m;
                    }
                }
            }

            return doc.Elements;
        }

        public async Task ForceRefreshTokenAsync()
        {
            var tenant = _tenantProvider.GetCurrentTenant();
            await ObtainOrRefreshTokenAsync(tenant, forceRefresh: true);
        }

        private async Task ObtainOrRefreshTokenAsync(Domain.Tenant tenant, bool forceRefresh = false)
        {
            // If we have a refresh token and it's not a fresh token, use refresh
            if (!string.IsNullOrEmpty(tenant.CloverRefreshToken) && (forceRefresh || tenant.CloverTokenExpiresAt <= DateTime.UtcNow))
            {
                var ok = await RefreshTokenAsync(tenant);
                if (ok) return;
            }

            // Otherwise obtain a token via client credentials or authorization_code depending on tenant configuration.
            // For demo, assume client_credentials grant is supported (Clover may require other flows; adapt as needed)
            if (!string.IsNullOrEmpty(tenant.CloverClientId) && !string.IsNullOrEmpty(tenant.CloverClientSecret))
            {
                await RequestClientCredentialsTokenAsync(tenant);
            }
            else
            {
                // No tenant credentials configured; nothing to do. Caller may fallback to global token.
                return;
            }
        }

        private async Task<bool> RefreshTokenAsync(Domain.Tenant tenant)
        {
            try
            {
                var tokenEndpoint = _options.BaseUrl.TrimEnd('/') + "/oauth2/token"; // example
                var body = new Dictionary<string, string>
                {
                    { "grant_type", "refresh_token" },
                    { "refresh_token", tenant.CloverRefreshToken! },
                    { "client_id", tenant.CloverClientId! },
                    { "client_secret", tenant.CloverClientSecret! }
                };

                var resp = await _http.PostAsync(tokenEndpoint, new FormUrlEncodedContent(body));
                if (!resp.IsSuccessStatusCode) return false;

                var json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("access_token", out var at)) tenant.CloverAccessToken = at.GetString();
                if (root.TryGetProperty("refresh_token", out var rt)) tenant.CloverRefreshToken = rt.GetString();
                if (root.TryGetProperty("expires_in", out var ex)) tenant.CloverTokenExpiresAt = DateTime.UtcNow.AddSeconds(ex.GetInt32());

                _db.Tenants.Update(tenant);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task RequestClientCredentialsTokenAsync(Domain.Tenant tenant)
        {
            var tokenEndpoint = _options.BaseUrl.TrimEnd('/') + "/oauth2/token"; // example

            var body = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", tenant.CloverClientId! },
                { "client_secret", tenant.CloverClientSecret! }
            };

            var resp = await _http.PostAsync(tokenEndpoint, new FormUrlEncodedContent(body));
            resp.EnsureSuccessStatusCode();

            var json = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("access_token", out var at)) tenant.CloverAccessToken = at.GetString();
            if (root.TryGetProperty("refresh_token", out var rt)) tenant.CloverRefreshToken = rt.GetString();
            if (root.TryGetProperty("expires_in", out var ex)) tenant.CloverTokenExpiresAt = DateTime.UtcNow.AddSeconds(ex.GetInt32());

            _db.Tenants.Update(tenant);
            await _db.SaveChangesAsync();
        }
    }
}
