using R6.Core.Notifications;

namespace R6.Core.Intefaces
{
    public interface INotificator
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}