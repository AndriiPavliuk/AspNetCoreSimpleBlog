using System;
using System.Collections.Generic;
using System.Text;
using Blog.Core.Articles.Model;
using Blog.Dto;
using Blog.Repository;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Blog.Core.Articles.Dto;

namespace Blog.Core.Articles
{
    public class ArticleService : IArticleService
    {
        private IRepository<Article> articleRep;

        public ArticleService(IRepository<Article> articleRep)
        {
            this.articleRep = articleRep;
        }

        public Task AddArticle(Article newArticle)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDto<Article>> GetArticleByPageAsync(QueryAriticelInputDto pagedResult)
        {
            var query = articleRep.GetAll();
            if (pagedResult.OnlyPublish)
            {
                query = query.Where(o => o.IsPublish == true);
            }

            var resultList = await query
                .OrderBy(pagedResult.Sorting ?? $"{nameof(Article.PostDate)} DESC")
                .Skip(pagedResult.SkipCount)
                .Take(pagedResult.MaxResultCount)
                .ToListAsync();
            var total = await articleRep.CountAsync();
            return new PagedResultDto<Article>(total, resultList);
        }

        public Task UpdateArticle(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
