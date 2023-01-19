using StarWars.Domain.Interfaces.Repositories;
using StarWars.Infrastructure.Data.Context;

namespace StarWars.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StarWarsContext _starWarsContext;

        public UnitOfWork(StarWarsContext starWarsContext) => _starWarsContext = starWarsContext;

        public bool Commit() => _starWarsContext.SaveChanges() > 0;

        public async Task<bool> CommitAsync(CancellationToken cancellationToken) =>
            await _starWarsContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
