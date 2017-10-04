using Blog.Core.Tags.Dto;
using Blog.Core.Tags.Model;
using Blog.Domain.Service;
using Blog.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Tags
{
    public interface ITagService : IDomainService
    {
        Task<PagedResultDto<TagDto>> GetTagByPageAsync(QueryTagInputDto queryInput);
        Task<List<Tag>> GetOrCreateTagsAsync(List<string> tags);
        Task DeleteTagAsync(int id);
    }
}
