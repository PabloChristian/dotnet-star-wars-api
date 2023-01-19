using Microsoft.EntityFrameworkCore;
using StarWars.Infrastructure.Data.Context;
using StarWars.Infrastructure.Data.Interfaces;
using StarWars.Shared.Kernel.Entity;
using System.Linq.Expressions;

namespace StarWars.Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly StarWarsContext Db;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(StarWarsContext realtimeChatContext)
        {
            Db = realtimeChatContext;
            DbSet = Db.Set<T>();
        }

        public void Add(T entity) => DbSet.Add(entity);
        public async Task AddAsync(T entity, CancellationToken cancellationToken) => await DbSet.AddAsync(entity, cancellationToken);
        public IQueryable<T> GetAll() => DbSet;
        public IQueryable<T> GetByExpression(Expression<Func<T, bool>> predicate) => DbSet.Where(predicate);
        public T GetById(Guid id) => DbSet?.Find(id);
        public void Remove(T entity) => DbSet.Remove(entity);
        public void Update(T entity) => DbSet.Update(entity);
    }
}
