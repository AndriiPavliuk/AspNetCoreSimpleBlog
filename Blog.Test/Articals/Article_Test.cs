using Blog.Core;
using Blog.Core.Articles;
using Blog.Core.Articles.Dto;
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
    public class Article_Test : BlogTestBase
    {
        private TestServer _server;
        private IArticleService articleService;

        public Article_Test()
        {
            AddTestDbContext<BlogDbContext>();
            this.articleService = _serviceProvider.GetRequiredService<IArticleService>();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetArticleByPageAsync_TestAsync()
        {
            BlogDbInitializer.Initialize(base.DbContext as BlogDbContext);
            var articles=await articleService.GetArticleByPageAsync(new QueryAriticelInputDto()
            {
                MaxResultCount = 10,
                SkipCount = 0
            });
            articles.Items.Count.ShouldBe(10);
            articles.TotalCount.ShouldBeGreaterThan(10);
        }

    }
}
