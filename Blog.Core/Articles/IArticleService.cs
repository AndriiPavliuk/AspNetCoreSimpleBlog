using Blog.Core.Articles.Dto;
using Blog.Core.Articles.Model;
using Blog.Core.Tags.Dto;
using Blog.Core.Tags.Model;
using Blog.Domain.Service;
using Blog.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Articles
{
    public interface IArticleService : IDomainService
    {
        Task<PagedResultDto<ArticleDto>> GetArticleByPageAsync(QueryAriticelInputDto pageQuery);
        Task<ArticleDto> AddArticleAsync(Article newArticle);
        Task UpdateArticleAsync(ArticleDto article);
        Task UpdateArticleTagsAsync(int id, IList<TagDto> tags);
        Task<ArticleDto> GetArticelAsync(int id);
        Task<List<ArticleDto>> GetArticelByTag(string tagName);
        Task DeleteArticleAsync(int id);
    }
}
