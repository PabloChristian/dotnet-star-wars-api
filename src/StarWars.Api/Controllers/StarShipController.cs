using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;
using StarWars.Application.Starships.Queries.GetStarshipList;
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

        /// <summary>
        /// Search for the list of starships
        /// </summary>
        /// <param name="manufacturer">filter by the manufacturer</param>
        /// <response code="200">Returns the list of starships</response>
        /// <response code="400">Returns in case there is any validation error</response>
        /// <response code="500">Internal server error, not being able to process the request</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStarships(
            [FromQuery] int page = 1,
            [FromQuery] string manufacturer = "")
        {
            var query = new GetStarshipQuery(page, manufacturer);
            return Response(await _mediator.SendCommandResult(query, new CancellationToken()));
        }
    }
}
