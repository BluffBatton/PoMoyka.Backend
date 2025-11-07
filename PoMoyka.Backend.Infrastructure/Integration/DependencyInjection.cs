using PoMoyka.Backend.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PoMoyka.Backend.Infrastructure.Integration.Authentication;
using PoMoyka.Backend.Infrastructure.Integration.External;

namespace PoMoyka.Backend.Infrastructure.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            // Регистрируем сервисы
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<ILiqPayService, LiqPayService>();
            return services;
        }
    }
}
