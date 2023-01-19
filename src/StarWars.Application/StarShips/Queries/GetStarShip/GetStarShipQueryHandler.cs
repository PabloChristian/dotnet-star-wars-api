using AutoMapper;
using MediatR;
using StarWars.Domain.ViewModels.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;

namespace StarWars.Application.Starships.Queries.GetStarshipList
{
    public class GetStarshipQueryHandler : IRequestHandler<GetStarshipQuery, List<StarshipViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IStarshipAdapter _starShipAdapter;

        public GetStarshipQueryHandler(IStarshipAdapter starShipAdapter, IMapper mapper)
        {
            _mapper = mapper;
            _starShipAdapter = starShipAdapter;
        }

        public async Task<List<StarshipViewModel>> Handle(GetStarshipQuery request, CancellationToken cancellationToken)
        {
            var starShipAdapter = new StarshipAdapter(_starShipAdapter);

            var starShipsData = await starShipAdapter.GetStarships();

            var starshipsViewModel = new List<StarshipViewModel>();
            return starshipsViewModel;
        }
    }
}