using AutoMapper;
using MediatR;
using StarWars.Domain.ViewModels.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;
using StarWars.Infrastructure.ServiceBus;
using StarWars.Shared.Kernel.Handler;
using StarWars.Shared.Kernel.Notifications;

namespace StarWars.Application.Starships.Queries.GetStarshipByManufacture
{
    public class GetStarshipByManufacturerQueryHandler : IRequestHandler<GetStarshipByManufacturerQuery, List<StarshipViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IStarshipAdapter _starShipAdapter;
        private readonly IMediatorHandler _mediatorHandler;

        public GetStarshipByManufacturerQueryHandler(IStarshipAdapter starShipAdapter, IMapper mapper, IMediatorHandler mediatorHandler)
        {
            _mapper = mapper;
            _starShipAdapter = starShipAdapter;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<List<StarshipViewModel>> Handle(GetStarshipByManufacturerQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                foreach (var error in request.GetErrors())
                    await _mediatorHandler.RaiseEvent(new DomainNotification(error.ErrorCode, error.ErrorMessage));
            }

            var starShipAdapter = new StarshipAdapter(_starShipAdapter);
            var starShipsData = await starShipAdapter.GetStarshipsByManufacturer(request.Page, request.Manufacturer);
            return _mapper.Map<List<StarshipViewModel>>(starShipsData);
        }
    }
}