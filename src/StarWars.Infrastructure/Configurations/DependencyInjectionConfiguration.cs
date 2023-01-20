using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;
using StarWars.Infrastructure.ServiceBus;
using StarWars.Infrastructure.Data.Context;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;
using System.Text.Json;
using StarWars.Domain.Interfaces.Repositories;
using StarWars.Infrastructure.Data;
using StarWars.Infrastructure.Data.Repositories;
using StarWars.Domain.Interfaces.Services;
using StarWars.Domain.Services;
using StarWars.Infrastructure.HttpAdapters;

namespace StarWars.Infrastructure.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.RegisterData();
            services.RegisterHandlers();
            services.RegisterHttpClients();
            services.RegisterApplicationServices();
        }

        private static void RegisterData(this IServiceCollection services)
        {
            services.AddDbContext<StarWarsContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void RegisterHandlers(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }

        private static void RegisterHttpClients(this IServiceCollection services)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                WriteIndented = true,
            };

            var settings = new RefitSettings(){ ContentSerializer = new SystemTextJsonContentSerializer(options)};

            services.AddRefitClient<IStarshipAdapter>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(HttpHelper.StarWarsApiUrl));
        }

        private static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
