using AutoMapper;
using MediatR;
using StarWars.Domain.ViewModels.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;
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
            return _mapper.Map<List<StarshipViewModel>>(starShipsData);
        }
    }
}