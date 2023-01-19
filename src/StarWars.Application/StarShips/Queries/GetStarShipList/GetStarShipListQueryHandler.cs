using AutoMapper;
using MediatR;
using StarWars.Domain.ViewModels.StarShips;
using StarWars.Infrastructure.HttpAdapters.StarShips;
using StarWars.Infrastructure.HttpAdapters.StarShips.Interfaces;

namespace StarWars.Application.StarShips.Queries.GetStarShipList
{
    public class GetStarShipListQueryHandler : IRequestHandler<GetStarShipListQuery, StarShipListViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IStarShipAdapter _starShipAdapter;

        public GetStarShipListQueryHandler(IStarShipAdapter starShipAdapter, IMapper mapper)
        {
            _mapper = mapper;
            _starShipAdapter = starShipAdapter;
        }

        public async Task<StarShipListViewModel> Handle(GetStarShipListQuery request, CancellationToken cancellationToken)
        {
            var starShipAdapter = new StarShipAdapter(_starShipAdapter);

            var starShipsData = await starShipAdapter.GetStarShips();

            var starshipsViewModel = new StarShipListViewModel {};
            return starshipsViewModel;
        }
    }
}