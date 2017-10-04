using Blog.Core;
using Blog.Core.Extensions;
using Blog.Domain.Entity;
using Blog.Domain.Service;
using Blog.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Test
{
    public static class BlogTestBaseExtension
    {
        public static void InitDb(this BlogTestBase blogTest)
        {
            blogTest.AddTestDbContext<BlogDbContext>();
            BlogDbInitializer.Initialize(blogTest.DbContext as BlogDbContext);
        }

        public static IRepository<T> GetRepository<T>(this BlogTestBase blogTest)
            where T : Entity
        {
            return blogTest.ServiceProvider.GetRequiredService<IRepository<T>>();
        }
        
        public static T GetDomainService<T>(this BlogTestBase blogTest)
            where T:IDomainService
        {
            return blogTest.ServiceProvider.GetRequiredService<T>();
        }
    }
}
