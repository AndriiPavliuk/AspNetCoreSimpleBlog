using Blog.Domain.Entity;
using Blog.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    /// <summary>
    /// Base class to implement <see cref="T:Abp.Domain.Repositories.IRepository`2" />.
    /// It implements some methods in most simple way.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IRepository where TEntity : class, IEntity<TPrimaryKey>
    {


        public abstract IQueryable<TEntity> GetAll();

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return this.GetAll();
        }

        public virtual List<TEntity> GetAllList()
        {
            return this.GetAll().ToList<TEntity>();
        }

        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult<List<TEntity>>(this.GetAllList());
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Where(predicate).ToList<TEntity>();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<List<TEntity>>(this.GetAllList(predicate));
        }

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(this.GetAll());
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            TEntity entity = this.FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString());
            }
            return entity;
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            TEntity entity = await this.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString());
            }
            return entity;
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<TEntity>(this.Single(predicate));
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            ParameterExpression lambdaParam = Expression.Parameter(typeof(TEntity));

            var expression = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(Expression.PropertyOrField(lambdaParam, "Id"), Expression.Constant(id, typeof(TPrimaryKey))),
                new ParameterExpression[]
                {
                    lambdaParam
                });
            
            return this.GetAll().FirstOrDefault(expression);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return Task.FromResult<TEntity>(this.FirstOrDefault(id));
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<TEntity>(this.FirstOrDefault(predicate));
        }

        public virtual TEntity Load(TPrimaryKey id)
        {
            return this.Get(id);
        }

        public abstract TEntity Insert(TEntity entity);

        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult<TEntity>(this.Insert(entity));
        }

        public virtual TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return this.Insert(entity).Id;
        }

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult<TPrimaryKey>(this.InsertAndGetId(entity));
        }

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            if (!entity.IsTransient())
            {
                return this.Update(entity);
            }
            return this.Insert(entity);
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            TEntity result;
            if (entity.IsTransient())
            {
                result = await this.InsertAsync(entity);
            }
            else
            {
                result = await this.UpdateAsync(entity);
            }
            return result;
        }

        public virtual TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            return this.InsertOrUpdate(entity).Id;
        }

        public virtual Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult<TPrimaryKey>(this.InsertOrUpdateAndGetId(entity));
        }

        public abstract TEntity Update(TEntity entity);

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult<TEntity>(this.Update(entity));
        }

        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            TEntity entity = this.Get(id);
            updateAction(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            TEntity tEntity = await this.GetAsync(id);
            TEntity tEntity2 = tEntity;
            await updateAction(tEntity2);
            return tEntity2;
        }

        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity)
        {
            this.Delete(entity);
            return Task.FromResult<int>(0);
        }

        public abstract void Delete(TPrimaryKey id);

        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            this.Delete(id);
            return Task.FromResult<int>(0);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (TEntity entity in this.GetAll().Where(predicate).ToList<TEntity>())
            {
                this.Delete(entity);
            }
        }

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            this.Delete(predicate);
            return Task.FromResult<int>(0);
        }

        public virtual int Count()
        {
            return this.GetAll().Count<TEntity>();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult<int>(this.Count());
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Where(predicate).Count<TEntity>();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<int>(this.Count(predicate));
        }

        public virtual long LongCount()
        {
            return this.GetAll().LongCount<TEntity>();
        }

        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult<long>(this.LongCount());
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Where(predicate).LongCount<TEntity>();
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<long>(this.LongCount(predicate));
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            ParameterExpression lambdaParam = Expression.Parameter(typeof(TEntity));
            return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(Expression.PropertyOrField(lambdaParam, "Id"), Expression.Constant(id, typeof(TPrimaryKey))), new ParameterExpression[]
            {
                lambdaParam
            });
        }
    }

}
