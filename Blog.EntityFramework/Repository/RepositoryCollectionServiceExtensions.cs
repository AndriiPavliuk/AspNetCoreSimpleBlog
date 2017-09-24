using Blog.Domain.Entity;
using Blog.Reflection;
using Blog.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.EntityFramework.Repository
{
    public static class RepositoryCollectionServiceExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            ITypeFinder typeFinder = new TypeFinder();
            var dbContextTypes = typeFinder.Find(a =>
              {
                  var b = a.IsAbstract == false
                       && a.IsClass
                       && typeof(DbContext).IsAssignableFrom(a);
                  return b;
              });
            foreach (var item in dbContextTypes)
            {
                RegisterForDbContext(item, services);
            }
            return services;
        }

        public static void RegisterForDbContext(Type dbContextType, IServiceCollection services)
        {
            //得到DbContext中DbSet属性的实体类
            var properties = dbContextType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(o => ReflectionHelper.IsAssignableToGenericType(o.PropertyType, typeof(DbSet<>))
              && ReflectionHelper.IsAssignableToGenericType(o.PropertyType.GetGenericArguments()[0],typeof(IEntity<>)))
                .Select(o => o.PropertyType.GetGenericArguments()[0]).ToList();

            foreach (var item in properties)
            {
                //得到实体类的主键
                var primaryKeyType = item.GetInterfaces().Where(i => i.IsGenericType
                && i.GetGenericTypeDefinition() == typeof(IEntity<>))
                .First().GetGenericArguments()[0];

                if (primaryKeyType == typeof(int))
                {
                    //注册IRepository<TEntity>与EfRepositoryBase<TDbContext, TEntity>
                    var repositoyGenericType = typeof(IRepository<>).MakeGenericType(item);
                    var implType = typeof(EfRepositoryBase<,>).MakeGenericType(dbContextType, item);
                    services.AddTransient(repositoyGenericType, implType);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
