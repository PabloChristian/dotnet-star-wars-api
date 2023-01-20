using AutoMapper;
using MediatR;
using Refit;
using StarWars.Domain.ViewModels.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;
using StarWars.Infrastructure.HttpAdapters.Starships.Results;
using StarWars.Infrastructure.ServiceBus;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;

namespace StarWars.Application.Starships.Queries.GetStarshipList
{
    public class GetStarshipQueryHandler : IRequestHandler<GetStarshipQuery, List<StarshipViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IStarshipAdapter _starShipAdapter;
        private readonly IMediatorHandler _mediatorHandler;

        public GetStarshipQueryHandler(IStarshipAdapter starShipAdapter, IMapper mapper, IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _starShipAdapter = starShipAdapter;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<List<StarshipViewModel>> Handle(GetStarshipQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                foreach (var error in request.GetErrors())
                    await _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage), cancellationToken);

                return null;
            }

            var starShipAdapter = new StarshipAdapter(_starShipAdapter);
            var starShipsData = await starShipAdapter.GetStarships(request.Page);

            if(!string.IsNullOrEmpty(request.Manufacturer))
            {
                FilterStarshipsByManufacturer(ref starShipsData, request.Manufacturer);
            }

            return _mapper.Map<List<StarshipViewModel>>(starShipsData);
        }

        private static void FilterStarshipsByManufacturer(ref List<StarshipDataResult> starShips, string manufacturer)
        {
            starShips = starShips.Where(
                x => x.Manufacturer.Equals(manufacturer, StringComparison.InvariantCultureIgnoreCase)
            ).ToList();
        }
    }
}