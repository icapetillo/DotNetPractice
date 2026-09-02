using CloverTest1.Data;
using Microsoft.EntityFrameworkCore;
using CloverTest1.Services;
using CloverTest1.Services.Clover;
using CloverTest1.Services.Filtering;
using Microsoft.OpenApi.Models;

namespace CloverTest1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Database (for demo use an in-memory or connection string placeholder)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=(localdb)\\mssqllocaldb;Database=CloverTest1;Trusted_Connection=True;";
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ITenantProvider, TenantProvider>();

            // Clover client
            builder.Services.Configure<CloverOptions>(builder.Configuration.GetSection("Clover"));
            var cloverOptions = new CloverOptions
            {
                BaseUrl = builder.Configuration["Clover:BaseUrl"] ?? "https://api.clover.com",
                MerchantId = builder.Configuration["Clover:MerchantId"] ?? string.Empty,
                AccessToken = builder.Configuration["Clover:AccessToken"] ?? string.Empty
            };

            builder.Services.AddSingleton(cloverOptions);
            builder.Services.AddHttpClient<ICloverClient, CloverClient>(client => client.BaseAddress = new Uri(cloverOptions.BaseUrl));

            builder.Services.AddScoped<IProductFilterService, ProductFilterService>();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clover WhiteLabel Core API", Version = "v1" });

                // Add tenant header parameter
                c.AddSecurityDefinition("X-Tenant-Id", new OpenApiSecurityScheme
                {
                    Description = "Tenant id header used to resolve data for a tenant",
                    Name = "X-Tenant-Id",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            var app = builder.Build();

            // Ensure DB created and seed demo tenants
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();
                DbSeed.EnsureSeed(db);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
