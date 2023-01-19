using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;
using StarWars.Application.Starships.Queries.GetStarshipList;
using StarWars.Application.Starships.Queries.GetStarshipByManufacture;

namespace StarWars.Api.Controllers
{
    [ApiController]
    [Route("api/starships")]
    public class StarshipController : BaseController
    {
        private const int DEFAULT_STARSHIPS_TAKE_VALUE = 30;

        public StarshipController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            ILogger<StarshipController> _)
            : base(notifications, mediator) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStarships(
            [FromQuery] int skip = 0,
            [FromQuery] int take = DEFAULT_STARSHIPS_TAKE_VALUE)
        {
            var query = new GetStarshipQuery { Skip = skip, Take = take };
            return Ok(await _mediator.SendCommandResult(query, new CancellationToken()));
        }

        [HttpGet("manufacturer/{manufacturerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStarshipsByManufacturer(
            [FromRoute] string manufacturer,
            [FromQuery] int skip = 0,
            [FromQuery] int take = DEFAULT_STARSHIPS_TAKE_VALUE)
        {
            var command = new GetStarshipByManufacturerQuery { Manufacturer = manufacturer, Skip = skip, Take = take };
            return Ok(await _mediator.SendCommandResult(command, new CancellationToken()));
        }
    }
}
