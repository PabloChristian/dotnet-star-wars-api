using AutoMapper;
using StarWars.Application.AutoMapper.Mappers;

namespace StarWars.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile(new StarshipEntityToDtoMapper());
                //cfg.AddProfile(new StarshipEntityToStarshipViewModelMapper());
                //cfg.AddProfile(new StarshipEntityToCreateStarshipViewModelMapper());


                cfg.AddProfile<MappingProfile>();
            });
        }
    }
}
