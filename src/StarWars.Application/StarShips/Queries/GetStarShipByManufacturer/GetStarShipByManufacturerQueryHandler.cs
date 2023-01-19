using AutoMapper;
using MediatR;
using StarWars.Domain.ViewModels.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;

namespace StarWars.Application.Starships.Queries.GetStarshipByManufacture
{
    public class GetStarshipByManufacturerQueryHandler : IRequestHandler<GetStarshipByManufacturerQuery, List<StarshipViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IStarshipAdapter _starShipAdapter;

        public GetStarshipByManufacturerQueryHandler(IStarshipAdapter starShipAdapter, IMapper mapper)
        {
            _mapper = mapper;
            _starShipAdapter = starShipAdapter;
        }

        public async Task<List<StarshipViewModel>> Handle(GetStarshipByManufacturerQuery request, CancellationToken cancellationToken)
        {
            var starShipAdapter = new StarshipAdapter(_starShipAdapter);
            var starShipsData = await starShipAdapter.GetStarshipsByManufacturer(request.Page, request.Manufacturer);
            return _mapper.Map<List<StarshipViewModel>>(starShipsData);
        }
    }
}