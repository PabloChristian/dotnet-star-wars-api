using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;
using StarWars.Application.Starships.Queries.GetStarshipList;
using StarWars.Application.Starships.Queries.GetStarshipByManufacture;
using Microsoft.AspNetCore.Authorization;

namespace StarWars.Api.Controllers
{
    [ApiController]
    [Route("api/starships")]
    public class StarshipController : BaseController
    {
        public StarshipController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            ILogger<StarshipController> _)
            : base(notifications, mediator) { }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStarships(
            [FromQuery] int page = 1)
        {
            var query = new GetStarshipQuery { Page = page };
            return Ok(await _mediator.SendCommandResult(query, new CancellationToken()));
        }

        [HttpGet("manufacturer/{manufacturer}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStarshipsByManufacturer(
            [FromRoute] string manufacturer,
            [FromQuery] int page = 1)
        {
            var command = new GetStarshipByManufacturerQuery { Page = page, Manufacturer = manufacturer };
            return Ok(await _mediator.SendCommandResult(command, new CancellationToken()));
        }
    }
}
