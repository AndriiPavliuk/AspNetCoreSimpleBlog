using Blog.Core;
using Blog.Core.Articles;
using Blog.Core.Articles.Dto;
using Blog.Core.Articles.Model;
using Blog.Core.Users.Model;
using Blog.EntityFramework.Repository;
using Blog.Repository;
using Blog.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Test.Articles
{
    public class ArticleService_Test : BlogTestBase
    {
        private TestServer _server;
        private IArticleService articleService;

        public ArticleService_Test()
        {
            this.InitDb();
            this.articleService = _serviceProvider.GetRequiredService<IArticleService>();
        }
       

        [Fact]
        public async Task GetArticleByPageAsync_TestAsync()
        {
            var articles=await articleService.GetArticleByPageAsync(new QueryAriticelInputDto()
            {
                MaxResultCount = 5,
                SkipCount = 0
            });
            articles.Items.Count.ShouldBe(5);
            articles.TotalCount.ShouldBeGreaterThan(5);
        }

        [Fact]
        public async Task GetArticle_TestAsync()
        {
            var article = await articleService.GetArticelAsync(1);
            article.Tags.ShouldNotBeNull();
            article.Tags.Count.ShouldBeGreaterThan(0);
        }


        [Fact]
        public async Task GetArticleByTag_TestAsync()
        {
            var article=await articleService.GetArticelByTag("标签1");
            article.ShouldNotBeNull();
        }
    }
}
