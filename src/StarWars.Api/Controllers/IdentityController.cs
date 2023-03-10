using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarWars.Application.Identity.Commands;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;
using StarWars.Shared.Kernel.Results;

namespace StarWars.Api.Controllers
{
    [ApiController]
    [Route("api/identity")]
    [AllowAnonymous]
    public class IdentityController : BaseController
    {
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            ILogger<IdentityController> logger)
            : base(notifications, mediator)
        {
            _logger = logger;
        }

        /// <summary>
        /// Identity control for register
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkReturn))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterUserCommand command)
        {
            var token = await _mediator.SendCommandResult(command);

            if (token != null)
            {
                _logger.LogInformation($"{command.Username} registered and logged in");
                return Response(token);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Identity control for login
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkReturn))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(AuthenticateUserCommand command)
        {
            var token = await _mediator.SendCommandResult(command);

            if (token != null)
            {
                _logger.LogInformation($"{command.Username} logged in");
                return Response(token);
            }

            return Unauthorized();
        }
    }
}
