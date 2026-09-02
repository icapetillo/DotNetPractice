namespace CloverTest1.Services.Clover
{
    public class CloverOptions
    {
        public string MerchantId { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty; // optional default token
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string BaseUrl { get; set; } = "https://api.clover.com";
    }
}
