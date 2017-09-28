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
using Blog.EntityFramework.Repository;
using Blog.Core.Tags.Model;

namespace Blog.Core.Articles
{
    public class ArticleService : IArticleService
    {
        private IRepository<Article> articleRep;
        private IRepository<Tag> tagRep;

        public ArticleService(IRepository<Article> articleRep,IRepository<Tag> tagRep)
        {
            this.articleRep = articleRep;
            this.tagRep = tagRep;
        }

        public Task AddArticle(Article newArticle)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> GetArticelAsync(int id)
        {
            var article = await articleRep.GetAllIncluding(o => o.Tags).Where(o => o.Id == id).FirstAsync();
            
            if (article.Tags == null)
            {
                article.Tags = new List<Tag>();
            }
            await articleRep.SaveChangesAsync();

            return article;
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
            var total = await query.CountAsync();
            return new PagedResultDto<Article>(total, resultList);
        }

        public async Task<List<Article>> GetArticelByTag(string tagName)
        {
            return await articleRep.GetAll().Include(o=>o.Tags).Where(o => o.Tags.Where(t => t.Name == tagName).Any()).ToListAsync();
        }
        public Task UpdateArticle(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
