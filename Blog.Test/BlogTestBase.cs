using Blog.Core;
using Blog.Core.Extensions;
using Blog.Domain.Service;
using Blog.EntityFramework.Repository;
using Blog.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Test
{
    public abstract class BlogTestBase
    {
        protected ServiceCollection _serviceCollection;
        protected ServiceProvider _serviceProvider;
        private DbContext _dbContext;
        public DbContext DbContext
        {
            set
            {
                this._dbContext = value;
            }
            get
            {
                if (_dbContext == null)
                {
                    throw new NullReferenceException();
                }
                return _dbContext;
            }
        }

        protected BlogTestBase(bool initialize = true)
        {
            if (initialize)
            {
                Initialize();
            }
        }
        protected void Initialize()
        {
            _serviceCollection = new ServiceCollection();

            _serviceCollection.AddBlogService();
            AdditionService(_serviceCollection);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
        protected virtual void AdditionService(ServiceCollection serviceCollection)
        {

        }
        public void AdditionService(Action<ServiceCollection> action)
        {
            action?.Invoke(_serviceCollection);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
        
        public void AddTestDbContext<T>(Action<DbContextOptionsBuilder> dbOptionBuilderAction = null) where T : DbContext
        {
            if (dbOptionBuilderAction == null)
            {

                dbOptionBuilderAction = new Action<DbContextOptionsBuilder>(o => o.UseInMemoryDatabase("ATestDb"));
            }

            _serviceCollection.AddDbContext<T>(dbOptionBuilderAction);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            DbContext = _serviceProvider.GetRequiredService<T>();
        }
    }
}
