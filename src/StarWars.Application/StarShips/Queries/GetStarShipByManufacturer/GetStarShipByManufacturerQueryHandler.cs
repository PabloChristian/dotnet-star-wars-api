using AutoMapper;
using MediatR;
using StarWars.Domain.ViewModels.StarShips;

namespace StarWars.Application.StarShips.Queries.GetStarShipByUser
{
    public class GetStarShipByUserQueryHandler : IRequestHandler<GetStarShipByUserQuery, StarShipListViewModel>
    {
        private readonly IMapper _mapper;

        public GetStarShipByUserQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<StarShipListViewModel> Handle(GetStarShipByUserQuery request, CancellationToken cancellationToken)
        {
            var starshipsViewModel = new StarShipListViewModel{};

            return starshipsViewModel;
        }
    }
}