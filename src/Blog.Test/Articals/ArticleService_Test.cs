using Blog.Admin.ViewModel;
using Blog.AutoMapper;
using Blog.Core;
using Blog.Core.Articles;
using Blog.Core.Articles.Dto;
using Blog.Core.Articles.Model;
using Blog.Core.Categorys.Model;
using Blog.Core.Relationship;
using Blog.Core.Tags.Dto;
using Blog.Core.Tags.Model;
using Blog.Core.Users.Model;
using Blog.EntityFramework.Repository;
using Blog.Repository;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Test.Articles
{
    public class ArticleService_Test : BlogTestBase
    {
        private TestServer _server;
        private IArticleService _articleService;
        private IRepository<Article> _articleRep;
        private IRepository<Category> _categoryRep;
        private IRepository<Tag> _tagRep;
        private IRepository<ArticleTag> _articleTagRep;

        public ArticleService_Test()
        {
            this.InitDb();
            this._articleService = this.GetDomainService<IArticleService>();
            this._articleRep = this.GetRepository<Article>();
            this._categoryRep = this.GetRepository<Category>();
            this._tagRep = this.GetRepository<Tag>();
            this._articleTagRep = this.GetRepository<ArticleTag>();
        }


        [Fact]
        public async Task GetArticleByPageAsync_TestAsync()
        {
            var articles = await _articleService.GetArticleByPageAsync(new QueryAriticelInputDto()
            {
                MaxResultCount = 5,
                SkipCount = 0,
                WithTags = true
            });
            articles.Items.Count.ShouldBe(5);
            articles.TotalCount.ShouldBeGreaterThan(5);
            var category = articles.Items.First().Category;
            category.ShouldNotBeNull();
            category.Name.Length.ShouldNotBe(0);

            foreach (var item in articles.Items)
            {
                item.Tags.Count.ShouldBeGreaterThan(0);
            }

        }

        [Fact]
        public async Task GetArticle_TestAsync()
        {
            var article = await _articleService.GetArticleAsync(1);
            article.Tags.ShouldNotBeNull();
            article.Tags.Count.ShouldBeGreaterThan(0);
            article.Tags.First().ShouldNotBeNull();
            article.Tags.First().Name.Length.ShouldBeGreaterThan(0);
            article.Category.ShouldNotBeNull();
            article.CheckAllPropertiesAreNotNullOrDefault(nameof(ArticleDto.ViewCount), nameof(ArticleDto.PostImage));
            

            var viewModel = article.MapTo<ArticleViewModel>();
            viewModel.Tags.ShouldNotBeNull();
            viewModel.Tags.Count.ShouldBeGreaterThan(0);
            viewModel.Tags.First().ShouldNotBeNull();
            viewModel.Tags.First().Name.Length.ShouldBeGreaterThan(0);
            viewModel.Category.ShouldNotBeNull();
            viewModel.CheckAllPropertiesAreNotNullOrDefault(nameof(ArticleViewModel.ViewCount), nameof(ArticleViewModel.PostImage));
        }

        [Fact]
        public async Task AddArticle_TestAsync()
        {
            var category = _categoryRep.GetAll().First();
            var newArticle = await _articleService.AddArticleAsync(new Article()
            {
                ArticleType = ArticleType.HTML,
                CategoryId = category.Id,
                IsPublish = true,
                PostDate = DateTime.Now,
                PostImage = "123",
                Content = "11111",
                Title = "2222",
            });
            newArticle.ArticleType.ShouldBe(ArticleType.HTML);
            newArticle.CheckAllPropertiesAreNotNullOrDefault(nameof(ArticleDto.ViewCount),nameof(ArticleDto.Tags));
            _articleRep.Get(newArticle.Id).ArticleType.ShouldBe(ArticleType.HTML);

        }

        [Fact]
        public async Task GetArticleByTag_TestAsync()
        {
            var articles = await _articleService.GetArticleByTag("标签1");
            articles.ShouldNotBeNull();
            articles.Count.ShouldBeGreaterThan(0);
            articles.First().Tags.Where(o => o.Name == "标签1").Any().ShouldBeTrue();
        }

        [Fact]
        public async Task UpdateArticleTags_TestAsync()
        {
            var article = _articleRep.GetAllList().First();
            article.ArticleTags.Count.ShouldBeGreaterThan(0);
            //已经有了1个标签,再加个标签,在更新时,保留1个,删除一个
            var tmpTag = _tagRep.Insert(new Tag()
            {
                Name = "临时标签"
            });
            _tagRep.SaveChanges();
            _articleTagRep.Insert(new ArticleTag()
            {
                TagId = tmpTag.Id,
                ArticleId = article.Id
            });
            _articleTagRep.SaveChanges();

            var inputTags = new List<TagDto>()
            {
                new TagDto(){ Name="新建标签1"},
                new TagDto(){ Name="新建标签2"},
                new TagDto(){Name="临时标签"},
            };
            await _articleService.UpdateArticleTagsAsync(article.Id, inputTags);
            article = _articleRep.GetAllList(o => o.Id == article.Id).First();
            article.ArticleTags.Count.ShouldBe(3);
            article.ArticleTags.Select(o => o.Tag)
                .ToList()
                .ForEach(o => (o.Name.StartsWith("新建标签") || o.Name == "临时标签").ShouldBeTrue());
            //测试空标签
            await _articleService.UpdateArticleTagsAsync(article.Id, null);
            article = _articleRep.GetAllList(o => o.Id == article.Id).First();
            article.ArticleTags.Count.ShouldBe(0);
        }
    }
}
