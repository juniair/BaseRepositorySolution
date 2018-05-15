using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BaseRepository
{
    public interface IRepository
    {
        void Add<TEntity>(TEntity entity) where TEntity : class;
        Task AddAsync<TEntity>(TEntity entity) where TEntity : class;
        IQueryable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        DbSet<TEntity> FindAll<TEntity>() where TEntity : class;
        Task<IQueryable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        TEntity FindById<TEntity, TIdentity>(TIdentity id) where TEntity : class;
        Task<TEntity> FindByIdAsync<TEntity, TIdentity>(TIdentity id) where TEntity : class;
        IQueryable<TEntity> GetRange<TEntity>(Expression<Func<TEntity, bool>> predicate, int skip, int take) where TEntity : class;
        Task<IQueryable<TEntity>> GetRangeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, int skip, int take) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}