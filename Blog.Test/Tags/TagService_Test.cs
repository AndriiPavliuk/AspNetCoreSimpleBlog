using Blog.Core;
using Blog.Core.Tags;
using Blog.Core.Tags.Model;
using Blog.Repository;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Test.Tags
{
    public class TagService_Test : BlogTestBase
    {
        private ITagService tagService;
        private IRepository<Tag> tagRep;

        public TagService_Test()
        {
            this.InitDb();
            tagService = _serviceProvider.GetRequiredService<ITagService>();
            tagRep = _serviceProvider.GetRequiredService<IRepository<Tag>>();
        }

        [Fact]
        public async Task GetOrUpdateTag_TestAsync()
        {
            var allTags=tagRep.GetAllList();
            var allTagStrs=allTags.Select(o => o.Name).ToList();
            allTagStrs.Add("新标签");


            (await tagService.GetOrCreateTagsAsync(allTagStrs)).Count.ShouldBe(allTagStrs.Count);
            tagRep.Count().ShouldBe(allTags.Count + 1);

        }


    }
}
