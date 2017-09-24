using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.EntityFramework.Repository
{
    public static class EfRepositoryExtensions
    {
        public static void SaveChanges<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
            where TEntity : class
        {
            var db = repository as IRepositoryWithDbContext;
            db.GetDbContext().SaveChanges();
        }
    }
}
