using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;
using StarWars.Application.StarShips.Queries.GetStarShipList;
using StarWars.Application.StarShips.Queries.GetStarShipByUser;

namespace StarWars.Api.Controllers
{
    [ApiController]
    [Route("api/starships")]
    public class StarShipController : BaseController
    {
        private const int DEFAULT_STARSHIPS_TAKE_VALUE = 30;

        public StarShipController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            ILogger<StarShipController> _)
            : base(notifications, mediator) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStarShips(
            [FromQuery] int skip = 0,
            [FromQuery] int take = DEFAULT_STARSHIPS_TAKE_VALUE)
        {
            var query = new GetStarShipListQuery { Skip = skip, Take = take };
            return Ok(await _mediator.SendCommandResult(query, new CancellationToken()));
        }

        [HttpGet("manufacturer/{manufacturerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStarShipsByManufacturerId(
            [FromRoute] string manufacturerId,
            [FromQuery] int skip = 0,
            [FromQuery] int take = DEFAULT_STARSHIPS_TAKE_VALUE)
        {
            var command = new GetStarShipByUserQuery { Manufacturer = manufacturerId, Skip = skip, Take = take };
            return Ok(await _mediator.SendCommandResult(command, new CancellationToken()));
        }
    }
}
