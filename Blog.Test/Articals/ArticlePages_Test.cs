using Blog.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Net.Http;

namespace Blog.Test.Articals
{
    public class ArticlePages_Test:BlogTestBase
    {
        private TestServer _server;
        private HttpClient _client;

        public ArticlePages_Test()
        {
            this.InitDb();
            _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public void EditPages_Test()
        {

        }
    }
}
