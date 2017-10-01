using Blog.Core.Tags.Model;
using Blog.Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Tags
{
    public interface ITagService : IDomainService
    {
        Task<List<Tag>> GetOrCreateTagsAsync(List<string> tags);
    }
}
