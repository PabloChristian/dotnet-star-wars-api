using StarWars.Domain.Entity;
using StarWars.Infrastructure.Data.Context;
using StarWars.Infrastructure.Data.Interfaces;

namespace StarWars.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(StarWarsContext starWarsContext) : base(starWarsContext) { }

    }
}
