using R6.Core.Communication.Mediator.Interfaces;
using R6.Core.Communication.Messages.Notifications;
using MediatR;
using System.Threading.Tasks;

namespace R6.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishDomainNotificationAsync<T>(T appNotification) 
            where T : DomainNotification
            => await _mediator.Publish(appNotification);
    }
}