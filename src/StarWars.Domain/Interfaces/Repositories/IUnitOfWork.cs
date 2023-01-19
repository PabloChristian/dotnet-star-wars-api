namespace StarWars.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        bool Commit();
        Task<bool> CommitAsync(CancellationToken cancellationToken);
    }
}
