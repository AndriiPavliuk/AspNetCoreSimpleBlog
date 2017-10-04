using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Tags.Model;
using Blog.Repository;
using System.Linq;
using Blog.EntityFramework.Repository;
using Blog.Core.Tags.Dto;
using Blog.Dto;
using Microsoft.EntityFrameworkCore;
using Blog.Core.Relationship;

namespace Blog.Core.Tags
{
    public class TagService : ITagService
    {
        private IRepository<Tag> _tagRep;
        private IRepository<ArticleTag> _articleTagRep;

        public TagService(IRepository<Tag> tagRep, IRepository<ArticleTag> articleTagRep)
        {
            this._tagRep = tagRep;
            this._articleTagRep = articleTagRep;
        }
        public async Task<List<Tag>> GetOrCreateTagsAsync(List<string> tags)
        {
            if (tags==null||tags.Count==0)
            {
                return new List<Tag>();
            }
            var exisitTags = _tagRep.GetAllList(o => tags.Contains(o.Name));
            var newTags = tags.Where(o => !(exisitTags.Select(t => t.Name).Contains(o))).Distinct();
            foreach (var item in newTags)
            {
                var newTag = new Tag() { Name = item };
                _tagRep.Insert(newTag);
                exisitTags.Add(newTag);
            }
            await _tagRep.SaveChangesAsync();
            return exisitTags;
        }
        public async Task<PagedResultDto<TagDto>> GetTagByPageAsync(QueryTagInputDto queryInput)
        {
            var query = _tagRep.GetAll();
            var resultList = await query
             .Include(o => o.ArticleTags)
             .Skip(queryInput.SkipCount)
             .Take(queryInput.MaxResultCount)
             .Select(c => new TagDto()
             {
                 Id = c.Id,
                 Name = c.Name,
                 ArticleCount = c.ArticleTags.Count
             })
             .ToListAsync();
            var total = await query.CountAsync();
            return new PagedResultDto<TagDto>(total, resultList);
        }

        public async Task DeleteTagAsync(int id)
        {
            var tag = await _tagRep.GetAsync(id);
            await _articleTagRep.DeleteAsync(o => o.TagId == tag.Id);
            await _tagRep.DeleteAsync(tag.Id);
            await _tagRep.SaveChangesAsync();
        }
    }
}
