using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;
using StarWars.Infrastructure.ServiceBus;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;

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
            services.AddRefitClient<IStarshipAdapter>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://swapi.dev/"));
        }
    }
}
