using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Infrastructure.Persistence.Integration.Authentication;
namespace PoMoyka.Backend.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetService<ApplicationDbContext>());
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
