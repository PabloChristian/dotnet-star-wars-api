using AutoMapper;
using MediatR;
using StarWars.Application.StarShips.Queries.GetStarShipList;
using StarWars.Domain.ViewModels.StarShips;

namespace StarWars.Application.StarShip.Queries.GetStarShipList
{
    public class GetStarShipListQueryHandler : IRequestHandler<GetStarShipListQuery, StarShipListViewModel>
    {
        private readonly IMapper _mapper;

        public GetStarShipListQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<StarShipListViewModel> Handle(GetStarShipListQuery request, CancellationToken cancellationToken)
        {
            var starshipsViewModel = new StarShipListViewModel {};
            return starshipsViewModel;
        }
    }
}