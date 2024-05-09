using Blblan.Common.Services;
using Blblan.Data;
using Blblan.Data.Entities;
using Blblan.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PredictionClientApp;

namespace Blblan.BusinessLayer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services, IConfigurationRoot configuration)
        {

            services.AddDbContext<BlblanDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));
            services.AddScoped<MessagesRepository>();
            services.AddScoped<ConversationRepository>();
            services.AddScoped<UserRepository>();

            return services;
        }
        
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<PredictionEngineClient>();
            return services;
        }
    }
}
