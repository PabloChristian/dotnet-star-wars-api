using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Shared.Kernel.Results;

namespace StarWars.Api.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;

        protected BaseController(INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();
        protected bool IsValidOperation() => !_notifications.HasNotifications();

        protected new IActionResult Response(object result)
        {
            if (IsValidOperation())
            {
                return Ok(new ApiOkReturn
                {
                    Success = true,
                    Data = result
                });
            }

            return BadRequest(new ApiBadReturn
            {
                Success = false,
                Errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors(CancellationToken cancellationToken)
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg, cancellationToken);
            }
        }

        protected void NotifyError(string code, string message, CancellationToken cancellationToken) 
            => _mediator.RaiseEvent(new DomainNotification(code, message), cancellationToken);
    }
}
