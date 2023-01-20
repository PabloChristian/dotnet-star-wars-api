using MediatR;

namespace StarWars.Shared.Kernel.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>, IDisposable
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler() => _notifications = new List<DomainNotification>();

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications() => _notifications;
        public virtual bool HasNotifications() => GetNotifications().Any();
        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
            GC.SuppressFinalize(this);
        }
    }
}
