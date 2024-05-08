using Blblan.Common.Services;
using Blblan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blblan.BusinessLayer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services, IConfigurationRoot configuration)
        {

            services.AddDbContext<BlblanDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

            return services;
        }
        
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
