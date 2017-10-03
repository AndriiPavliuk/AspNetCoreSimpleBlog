using Blog.Core;
using Blog.Core.Relationship;
using Blog.Core.Tags;
using Blog.Core.Tags.Dto;
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
        private ITagService _tagService;
        private IRepository<Tag> _tagRep;
        private IRepository<ArticleTag> _articleTagRep;

        public TagService_Test()
        {
            this.InitDb();
            _tagService = this.GetDomainService<ITagService>();
            _tagRep = this.GetRepository<Tag>();
            _articleTagRep = this.GetRepository<ArticleTag>();
        }

        [Fact]
        public async Task GetOrUpdateTag_TestAsync()
        {
            var allTags = _tagRep.GetAllList();
            var allTagStrs = allTags.Select(o => o.Name).ToList();
            allTagStrs.Add("新标签");


            (await _tagService.GetOrCreateTagsAsync(allTagStrs)).Count.ShouldBe(allTagStrs.Count);
            _tagRep.Count().ShouldBe(allTags.Count + 1);

        }
        [Fact]
        public async Task GetTagsByPage_TestAsync()
        {
            var query = new QueryTagInputDto()
            {
                MaxResultCount = 5,
                SkipCount = 0,
            };
            var result = await _tagService.GetTagByPageAsync(query);
            result.TotalCount.ShouldBeGreaterThan(5);
            foreach (var item in result.Items)
            {
                item.ArticleCount.ShouldBeGreaterThan(0);
            }

        }
        [Fact]
        public async Task DeleteTag_Async()
        {
            var tag=_tagRep.GetAllList().First();
            tag.ArticleTags.Count.ShouldBeGreaterThan(0);
            await _tagService.DeleteTagAsync(tag.Id);
            _articleTagRep.GetAllList(o => o.TagId == tag.Id).Count.ShouldBe(0);

        }

    }
}
