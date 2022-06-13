using R6.API.ViewModels;
using R6.Core.Communication.Handlers;
using R6.Core.Communication.Messages.Notifications;
using R6.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace R6.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _domainNotificationHandler;

        protected BaseController(
           INotificationHandler<DomainNotification> domainNotificationHandler)
        {
            _domainNotificationHandler = (DomainNotificationHandler)domainNotificationHandler;
        }

        protected bool HasNotifications()
           => _domainNotificationHandler.HasNotifications();

        protected ObjectResult Created(dynamic responseObject)
            => StatusCode(201, responseObject);

        protected ObjectResult Result()
        {
            var notification = _domainNotificationHandler
                .Notifications
                .FirstOrDefault();

            return StatusCode(GetStatusCodeByNotificationType(notification.Type),
                new ResultViewModel
                {
                    Message = notification.Message,
                    Success = false,
                    Data = new { }
                });
        }

        private int GetStatusCodeByNotificationType(DomainNotificationType errorType)
        {
            return errorType switch
            {
                //Conflict
                DomainNotificationType.OperatorAlreadyExists
                    => 409,

                //Unprocessable Entity
                DomainNotificationType.UserInvalid
                    => 422,

                DomainNotificationType.InvalidEnum
                    => 422,

                //Not Found
                DomainNotificationType.UserNotFound
                    => 404,

                (_) => 500,
            };
        }
    }
}