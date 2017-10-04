using Blog.Core;
using Blog.Core.Articles.Model;
using Blog.Domain.Entity;
using Blog.EntityFramework.Repository;
using Blog.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Blog.Test.Infrastructure
{
    public interface IFoo
    {

    }
    public class FooDbContext : DbContext
    {
        public DbSet<Foo> Foo { set; get; }
        public FooDbContext(DbContextOptions<FooDbContext> options):base(options)
        {

        }
    }
    public class Foo : Entity, IFoo
    {
        public string Value { get; set; }
    }

    public class BarWithFooRepository
    {
        public BarWithFooRepository(IRepository<Foo> repository)
        {
            this.Repository = repository;
        }

        public IRepository<Foo> Repository { get; }
    }

    public class Repository_Test
    {
        [Fact]
        public void DI_Test()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<FooDbContext>(o => o.UseInMemoryDatabase(databaseName: "DI_Test"));
            services.AddTransient<IRepository<Foo>, EfRepositoryBase<FooDbContext, Foo>>();
            var provider = services.BuildServiceProvider();
            var instance = provider.GetService<IRepository<Foo>>();

            var db = provider.GetService<FooDbContext>();
            db.ShouldNotBeNull();

            //测试仓储注入
            instance.ShouldNotBeNull();

            instance.Insert(new Foo() { Value = "666" });
            instance.SaveChanges();

            db.Foo.Count().ShouldBe(1);
            db.Foo.First().Value.ShouldBe("666");
        }

        [Fact]
        public void AddRepository_Test()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<FooDbContext>(o => o.UseInMemoryDatabase(databaseName: "AddRepository_Test"));
            services.AddRepository();
            services.AddTransient<BarWithFooRepository>();
            var provider = services.BuildServiceProvider();

            var bar=provider.GetService<BarWithFooRepository>();
            bar.Repository.ShouldNotBeNull();
            bar.Repository.Insert(new Foo() { Value = "555" });
            bar.Repository.SaveChanges();

            var db = provider.GetService<FooDbContext>();
            db.Foo.First().Value.ShouldBe("555");
        }
    }
}
