using Blog.Core;
using Blog.Core.Articles.Model;
using Blog.EntityFramework.Repository;
using Blog.Repository;
using Blog.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.ComponentModel.Design;
using System.Linq;
using Xunit;

namespace Blog.Test.Articles
{
    public class Article_Test
    {
        private TestServer _server;

        public Article_Test()
        {
            // _server= new TestServer(new WebHostBuilder().UseStartup<Startup>());
        }
        [Fact]
        public void Db_Test()
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
            using (var db = new BlogDbContext(options))
            {
                db.Articles.Add(new Article()
                {
                    Content = "内容"
                });
                db.SaveChanges();
            }

            using (var db = new BlogDbContext(options))
            {
                var article = db.Articles.FirstOrDefault();
                article.Content.ShouldBe("内容");
            }
        }
       
    }
}
