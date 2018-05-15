using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public abstract class AbstractRepository<TDataBase> : IRepository where TDataBase : DbContext
    {
        protected readonly DbContext _context;
        protected DbContext Context {  get { return _context as TDataBase; } }

        public AbstractRepository(DbContext context)
        {
            _context = context;
        }

        private DbSet<TEntity> Table<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        public virtual void Add<TEntity>(TEntity entity) where TEntity : class
        {
            Table<TEntity>().Add(entity);
        }

        public virtual async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await Table<TEntity>().AddAsync(entity);
        }

        public TEntity FindById<TEntity, TIdentity>(TIdentity id) where TEntity : class
        {
            return Table<TEntity>().Find(id);
        }

        public virtual async Task<TEntity> FindByIdAsync<TEntity, TIdentity>(TIdentity id) where TEntity : class
        {
            return await Table<TEntity>().FindAsync(id);
        }

        public virtual DbSet<TEntity> FindAll<TEntity>() where TEntity : class
        {
            return Table<TEntity>();
        }

        public virtual IQueryable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            try
            {
                return Table<TEntity>().Where(predicate);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        public virtual async Task<IQueryable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var list = await Table<TEntity>().Where(predicate).ToListAsync();
            return list.AsQueryable();
        }

        public virtual void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Table<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Table<TEntity>().RemoveRange(entities);
        }

        public virtual IQueryable<TEntity> GetRange<TEntity>(Expression<Func<TEntity, bool>> predicate, int skip, int take) where TEntity : class
        {
            try
            {
                return Table<TEntity>().Where(predicate).Skip(skip).Take(take);
            }
            catch (AggregateException e)
            {
                throw e;
            }
        }

        public virtual async Task<IQueryable<TEntity>> GetRangeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, int skip, int take) where TEntity : class
        {
            try
            {
                var list = await Table<TEntity>().Where(predicate).Skip(skip).Take(take).ToListAsync();
                return list.AsQueryable();
            }
            catch (AggregateException e)
            {
                throw e;
            }
        }
    }
}
