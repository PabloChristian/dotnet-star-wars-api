using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
        }

        private static void RegisterHandlers(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }
    }
}
