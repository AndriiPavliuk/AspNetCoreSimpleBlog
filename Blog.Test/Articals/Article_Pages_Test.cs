using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Net.Http;
using Blog.Repository;
using Blog.Core.Articles.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Blog.Admin.Pages.Articles;
using Shouldly;
using Microsoft.Extensions.Configuration;
using System.IO;
using Xunit.Abstractions;
using Blog.AutoMapper;
using Blog.Admin.ViewModel;
using Blog.Core.Tags.Model;

namespace Blog.Test.Articles
{
    public class Article_Pages_Test : BlogTestBase
    {
        private ITestOutputHelper _testOutputHelper;
        private TestServer _server;
        private HttpClient _client;
        private IRepository<Article> _articleRep;

        public Article_Pages_Test()
        {
            this.InitDb();

            _articleRep = _serviceProvider.GetRequiredService<IRepository<Article>>();
        }

        protected override void AdditionService(ServiceCollection serviceCollection)
        {
            //AddMVC 为什么PagesModel没注册?
            serviceCollection.AddMvc();
            serviceCollection.AddTransient<EditModel>();
        }
        [Fact]
        public async System.Threading.Tasks.Task PutArticle_TestAsync()
        {
            var editPage = _serviceProvider.GetRequiredService<EditModel>();
            editPage.ShouldNotBeNull();

            var article = _articleRep.GetAllList().First();
            var newTags = article.Tags.ToList();
            newTags.Add(new Tag() { Name = "测试标签" });
            editPage.CurrentArticle = new ArticleViewModel()
            {
                Id = article.Id,
                Title = "测试标题",
                Tags = newTags
            };

            await editPage.OnPutAsync();
            var article2 = _articleRep.GetAllList().First();

            //Act
            article2.Title.ShouldBe("测试标题");
            article2.Content.ShouldBe(article.Content);
            article2.Tags.Count.ShouldBeGreaterThan(1);
            article2.Tags.Select(o => o.Name).ShouldContain("测试标签");
        }

        #region 为什么请求总是Not Found?
        //public Article_Pages_Test(ITestOutputHelper testOutputHelper)
        //{
        //    this.InitDb();
        //    _testOutputHelper = testOutputHelper;
        //    _server = new TestServer(new WebHostBuilder()
        //    .ConfigureAppConfiguration(o =>
        //    {
        //        o.SetBasePath(Path.GetFullPath("../../../")).AddJsonFile("appsettings.json");
        //    })
        //    .UseStartup<Admin.Startup>());
        //    _client = _server.CreateClient();

        //    //与Client数据库上下文不一致
        //    _articleRep = _serviceProvider.GetRequiredService<IRepository<Article>>();
        //}

        //[Fact]
        //public async System.Threading.Tasks.Task PutArticle_TestAsync()
        //{
        //    var article = _articleRep.GetAllList().First();
        //    var result = await _client.GetAsync($"/Articles/Edit/{article.Id.ToString()}");
        //    _testOutputHelper.WriteLine(result.StatusCode.ToString());
        //    result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

        //    //var postContentDic = new Dictionary<string, string>()
        //    //{
        //    //    [$"{nameof(EditModel.CurrentArticle)}.{nameof(Article.Id)}"] = article.Id.ToString(),
        //    //    [$"{nameof(EditModel.CurrentArticle)}.{nameof(Article.Title)}"] = "测试标题"
        //    //};
        //    //var result = await _client.PutAsync($"/Articles/Edit/{article.Id.ToString()}", new FormUrlEncodedContent(postContentDic));
        //    //_testOutputHelper.WriteLine(result.StatusCode.ToString());
        //    //result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
        //}
        #endregion

    }
}
