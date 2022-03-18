using R6.Core.Communication.Messages.Notifications;
using System.Threading.Tasks;

namespace R6.Core.Communication.Mediator.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishDomainNotificationAsync<T>(T appNotification)
            where T : DomainNotification;
    }
}