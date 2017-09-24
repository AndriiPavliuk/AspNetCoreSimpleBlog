using Blog.Domain.Entity;
using Blog.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.EntityFramework.Repository
{
    public class EfRepositoryBase<TDbContext, TEntity, TPrimaryKey> 
        : RepositoryBase<TEntity, TPrimaryKey> , IRepositoryWithDbContext
        where TDbContext : DbContext 
        where TEntity : class, IEntity<TPrimaryKey>
    {
        //, IRepositoryWithDbContext
        //private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        private TDbContext _context;
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        public virtual TDbContext Context
        {
            get
            {
                return _context;
            }
        }

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        public virtual DbSet<TEntity> Table
        {
            get
            {
                return this.Context.Set<TEntity>();
            }
        }

        ///// <summary>
        ///// Constructor
        ///// </summary>
        ///// <param name="dbContextProvider"></param>
        //public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        //{
        //    this._dbContextProvider = dbContextProvider;
        //}

        public EfRepositoryBase(TDbContext dbContext)
        {
            _context = dbContext;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return this.Table;
        }

        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            if (propertySelectors==null|| propertySelectors.Length==0)
            {
                return this.GetAll();
            }
            IQueryable<TEntity> query = this.GetAll();
            for (int i = 0; i < propertySelectors.Length; i++)
            {
                Expression<Func<TEntity, object>> propertySelector = propertySelectors[i];
                query = query.Include(propertySelector);
            }
            return query;
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await this.GetAll().ToListAsync<TEntity>();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.GetAll().Where(predicate).ToListAsync<TEntity>();
        }

        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.GetAll().SingleAsync(predicate);
        }

        //public override async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        //{
        //    return await this.GetAll().FirstOrDefaultAsync(AbpRepositoryBase<TEntity, TPrimaryKey>.CreateEqualityExpressionForId(id));
        //}

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.GetAll().FirstOrDefaultAsync(predicate);
        }

        public override TEntity Insert(TEntity entity)
        {
            return this.Table.Add(entity).Entity;
        }

        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult<TEntity>(this.Insert(entity));
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = this.Insert(entity);
            if (entity.IsTransient())
            {
                this.Context.SaveChanges();
            }
            return entity.Id;
        }

        public override async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            TEntity tEntity = await this.InsertAsync(entity);
            entity = tEntity;
            if (entity.IsTransient())
            {
                await this.Context.SaveChangesAsync();
            }
            return entity.Id;
        }

        public override TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = this.InsertOrUpdate(entity);
            if (entity.IsTransient())
            {
                this.Context.SaveChanges();
            }
            return entity.Id;
        }

        public override async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            TEntity tEntity = await this.InsertOrUpdateAsync(entity);
            entity = tEntity;
            if (entity.IsTransient())
            {
                await this.Context.SaveChangesAsync();
            }
            return entity.Id;
        }

        public override TEntity Update(TEntity entity)
        {
            this.AttachIfNot(entity);
            this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
            return entity;
        }

        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            this.AttachIfNot(entity);
            this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
            return Task.FromResult<TEntity>(entity);
        }

        public override void Delete(TEntity entity)
        {
            this.AttachIfNot(entity);
            this.Table.Remove(entity);
        }

        public override void Delete(TPrimaryKey id)
        {
            TEntity entity = this.Table.Local.FirstOrDefault((TEntity ent) => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
            if (entity == null)
            {
                entity = this.FirstOrDefault(id);
                if (entity == null)
                {
                    return;
                }
            }
            this.Delete(entity);
        }

        public override async Task<int> CountAsync()
        {
            return await this.GetAll().CountAsync<TEntity>();
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.GetAll().Where(predicate).CountAsync<TEntity>();
        }

        public override async Task<long> LongCountAsync()
        {
            return await this.GetAll().LongCountAsync<TEntity>();
        }

        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.GetAll().Where(predicate).LongCountAsync<TEntity>();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!this.Table.Local.Contains(entity))
            {
                this.Table.Attach(entity);
            }
        }

        public DbContext GetDbContext()
        {
            return this.Context;
        }
    }

    public class EfRepositoryBase<TDbContext, TEntity> : 
        EfRepositoryBase<TDbContext, TEntity, int>, 
        IRepository<TEntity>, 
        IRepository<TEntity, int>, 
        IRepository 
        where TDbContext : DbContext 
        where TEntity : class, IEntity<int>
    {
        public EfRepositoryBase(TDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
