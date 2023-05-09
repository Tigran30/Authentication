using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Authentication.Repository
{
    public class Repository<T> where T : class
    {
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        private DbSet<T> DbSet { set; get; }

        private DbContext Context { set; get; }

        public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
        public async Task AddRange(IEnumerable<T> entities) => await DbSet.AddRangeAsync(entities);
        public EntityEntry<T> Update(T entity) => DbSet.Update(entity);
        public void UpdateRange(T[] entities) => DbSet.UpdateRange(entities);
        public IQueryable<T> GetAll() => DbSet;
        public T? FirstOrDefault(Expression<Func<T, bool>> predicate) => DbSet.FirstOrDefault(predicate);
        public void Delete(T entity) => DbSet.Remove(entity);
        public async Task SaveAsync() => await Context.SaveChangesAsync();

    }
}
