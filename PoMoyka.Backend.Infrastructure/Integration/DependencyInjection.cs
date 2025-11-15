using PoMoyka.Backend.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PoMoyka.Backend.Infrastructure.Integration.Authentication;
using PoMoyka.Backend.Infrastructure.Integration.External;
using Microsoft.Extensions.Options;
using PoMoyka.Backend.Infrastructure.Integration.FileStorage;
using Supabase;

namespace PoMoyka.Backend.Infrastructure.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            // Привязываем секцию "Supabase" к опциям
            services.Configure<SupabaseStorageOptions>(configuration.GetSection("Supabase"));

            // Регистрируем Supabase client с инициализацией
            services.AddSingleton(provider =>
            {
                var options = provider.GetRequiredService<IOptions<SupabaseStorageOptions>>().Value;

                if (string.IsNullOrEmpty(options.Url) || string.IsNullOrEmpty(options.Key))
                    throw new InvalidOperationException("Supabase configuration is missing. Please add Supabase:Url and Supabase:Key to your configuration.");

                var supabaseOptions = new SupabaseOptions
                {
                    AutoRefreshToken = true,
                    AutoConnectRealtime = true
                };

                var supabaseClient = new Supabase.Client(options.Url, options.Key, supabaseOptions);
                supabaseClient.InitializeAsync().GetAwaiter().GetResult();
                return supabaseClient;
            });

            // Регистрируем сервисы
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<ILiqPayService, LiqPayService>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
