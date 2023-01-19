using StarWars.Domain.Entity;
using StarWars.Domain.Interfaces.Repositories;
using StarWars.Infrastructure.Data.Context;

namespace StarWars.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(StarWarsContext starWarsContext) : base(starWarsContext) { }
    }
}
