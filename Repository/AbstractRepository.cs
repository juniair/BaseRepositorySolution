using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class AbstractRepository<TDataBase> : IRepository where TDataBase : DbContext
    {
        protected readonly DbContext _context;
        protected DbContext Context {  get { return _context; } }

        public AbstractRepository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 변경된 DB를 저장 합니다.
        /// </summary>
        /// <returns>성공 : true, 실패 : false</returns>
        public bool SaveChanges()
        {
            return Context.SaveChanges() > 0;
        }


        /// <summary>
        /// 변경된 DB를 비동기로 저장 합니다.
        /// </summary>
        /// <returns>성공 : true, 실패 : false</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }

        protected DbSet<TEntity> Table<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }


        /// <summary>
        /// 해당 테이블에 데이터를 추가합니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블 명</typeparam>
        /// <param name="entity">추가 될 테이블 데이터</param>
        public virtual void Add<TEntity>(TEntity entity) where TEntity : class
        {
            Table<TEntity>().Add(entity);
        }

        /// <summary>
        /// 해당 테이블에 데이터를 비동기로 추가합니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블 명</typeparam>
        /// <param name="entity">추가 될 테이블 데이터</param>
        public virtual async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await Table<TEntity>().AddAsync(entity);
        }

        /// <summary>
        /// 해당 테이블에서 ID로 데이터를 찾습니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블 명</typeparam>
        /// <typeparam name="TIdentity">ID 데이터 타입</typeparam>
        /// <param name="id">테이블에서 찾을 데이터의 ID</param>
        /// <returns>찾으면 데이터를 반환하고 못찾을 시 null을 반환</returns>
        public TEntity FindById<TEntity, TIdentity>(TIdentity id) where TEntity : class
        {
            return Table<TEntity>().Find(id);
        }

        /// <summary>
        /// 해당 테이블에서 ID로 데이터를 비동기로 찾습니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블 명</typeparam>
        /// <typeparam name="TIdentity">ID 데이터 타입</typeparam>
        /// <param name="id">테이블에서 찾을 데이터의 ID</param>
        /// <returns>찾으면 데이터를 반환하고 못찾을 시 null을 반환</returns>
        public virtual async Task<TEntity> FindByIdAsync<TEntity, TIdentity>(TIdentity id) where TEntity : class
        {
            return await Table<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// 해당 테이블의 데이터를 모두 가져옵니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블 명</typeparam>
        /// <returns>테이블 데이터들</returns>
        public virtual DbSet<TEntity> FindAll<TEntity>() where TEntity : class
        {
            return Table<TEntity>();
        }

        /// <summary>
        /// 조건에 맞는 테이블의 데이터를 찾습니다. 조건식에 문제가 발생시 예외를 발생 시킵니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블명</typeparam>
        /// <param name="predicate">찾을 조건 식</param>
        /// <returns>조건식에 필터된 테이블을 반환</returns>
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

        /// <summary>
        /// 조건에 맞는 테이블의 데이터를 비동기로 찾습니다. 조건식에 문제가 발생시 예외를 발생 시킵니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블명</typeparam>
        /// <param name="predicate">찾을 조건 식</param>
        /// <returns>조건식에 필터된 테이블을 반환</returns>
        public virtual async Task<IQueryable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            try
            {
                var list = await Table<TEntity>().Where(predicate).ToListAsync();
                return list.AsQueryable();
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            
        }

        /// <summary>
        /// 테이블에 선택된 데이터를 삭제합니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블명</typeparam>
        /// <param name="entity">삭제 할 테이블 데이터</param>
        public virtual void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Table<TEntity>().Remove(entity);
        }

        /// <summary>
        /// 테이블에서 선택된 데이터들을 삭제합니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블 명</typeparam>
        /// <param name="entities">삭제할 테이블 데이터 들</param>
        public virtual void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Table<TEntity>().RemoveRange(entities);
        }

        /// <summary>
        /// 테이블에서 조건에 맞는 데이터들의 일부를 정렬하지 않는 상태로 얻습니다. 문제가 발생시 예외를 발생합니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블 명</typeparam>
        /// <param name="predicate">찾을 데이터의 조건식</param>
        /// <param name="skip">생략 횟수</param>
        /// <param name="take">찾을 갯수</param>
        /// <returns>조건에 맞는 데이터들의 일부</returns>
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

        /// <summary>
        /// 테이블에서 조건에 맞는 데이터들의 일부를 정렬하지 않는 상태로 비동기로 얻습니다. 문제가 발생시 예외를 발생합니다.
        /// </summary>
        /// <typeparam name="TEntity">DB 테이블 명</typeparam>
        /// <param name="predicate">찾을 데이터의 조건식</param>
        /// <param name="skip">생략 횟수</param>
        /// <param name="take">찾을 갯수</param>
        /// <returns>조건에 맞는 데이터들의 일부</returns>
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
