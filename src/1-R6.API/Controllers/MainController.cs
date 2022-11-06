using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using R6.Core.Intefaces;
using R6.Core.Notificacoes;

namespace R6.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotificator _notificator;
    public readonly IUser AppUser;

    protected Guid UserId { get; set; }
    protected bool IsAuthenticatedUser { get; set; }

    protected MainController(INotificator notificador, 
                             IUser appUser)
    {
        _notificator = notificador;
        AppUser = appUser;

        if (appUser.IsAuthenticated())
        {
            UserId = appUser.GetUserId();
            IsAuthenticatedUser = true;
        }
    }

    protected bool ValidOperation()
    {
        return !_notificator.HasNotification();
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notificator.GetNotifications().Select(n => n.Mensagem)
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if(!modelState.IsValid) 
            NotifyInvalidModel(modelState);

        return CustomResponse();
    }

    protected void NotifyInvalidModel(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(errorMsg);
        }
    }

    protected void NotifyError(string mensagem)
    {
        _notificator.Handle(new Notification(mensagem));
    }
}
