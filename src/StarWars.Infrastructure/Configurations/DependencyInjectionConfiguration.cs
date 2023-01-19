using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Refit;
using StarWars.Infrastructure.Configurations;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;
using StarWars.Infrastructure.ServiceBus;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;
using System.Text.Json;

namespace StarWars.Infrastructure.InversionOfControl
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.RegisterHandlers();
            services.RegisterHttpClients();
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

            var settings = new RefitSettings()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(options)
            };

            services.AddRefitClient<IStarshipAdapter>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri("https://swapi.dev/"));
        }
    }
}
