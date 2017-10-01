using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Tags.Model;
using Blog.Repository;
using System.Linq;
using Blog.EntityFramework.Repository;

namespace Blog.Core.Tags
{
    public class TagService : ITagService
    {
        private IRepository<Tag> tagRep;

        public TagService(IRepository<Tag> tagRep)
        {
            this.tagRep = tagRep;
        }
        public async Task<List<Tag>> GetOrCreateTagsAsync(List<string> tags)
        {
            var exisitTags = tagRep.GetAllList(o => tags.Contains(o.Name));
            //HACK exisitTags.Select(t => t.Name)会被执行多次吗
            var newTags = tags.Where(o => !(exisitTags.Select(t => t.Name).Contains(o))).Distinct();
            foreach (var item in newTags)
            {
                var newTag = new Tag() { Name = item };
                tagRep.Insert(newTag);
                exisitTags.Add(newTag);
            }
            await tagRep.SaveChangesAsync();
            return exisitTags;
        }
    }
}
