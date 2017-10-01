using System;
using System.Collections.Generic;
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
using Blog.Core.Articles.ContentProcessor;
using Blog.Core.Tags;

namespace Blog.Core.Articles
{
    public class ArticleService : IArticleService
    {
        private IRepository<Article> articleRep;
        private ITagService tagService;
        private IArticleContentProcessorProvider contentProcessorProvider;

        public ArticleService(
            IRepository<Article> articleRep,
            ITagService tagService,
            IArticleContentProcessorProvider contentProcessorProvider)
        {
            this.articleRep = articleRep;
            this.tagService = tagService;
            this.contentProcessorProvider = contentProcessorProvider;
        }

        public async Task<Article> AddArticleAsync(Article newArticle)
        {
            if (newArticle.Title.IsNullOrWhiteSpace())
            {
                newArticle.Title = "无标题";
            }
            if (newArticle.Content.IsNullOrWhiteSpace())
            {
                newArticle.Content = "";
            }
            if (newArticle.Summary.IsNullOrWhiteSpace())
            {
                newArticle.Summary = newArticle.Content.Substring(0, Math.Min(150, newArticle.Content.Length));
            }
            newArticle.PostDate = DateTime.Now;
            newArticle.UpdateDate = newArticle.PostDate;
            var result = await articleRep.InsertAsync(newArticle);
            await articleRep.SaveChangesAsync();
            return result;
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
            if (pagedResult.WithTags)
            {
                query = query.Include(o => o.Tags);
            }
            if (pagedResult.WithCategory)
            {
                query = query.Include(o => o.Category);
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
            return await articleRep.GetAll().Include(o => o.Tags).Where(o => o.Tags.Where(t => t.Name == tagName).Any()).ToListAsync();
        }
        public async Task UpdateArticleAsync(Article article)
        {
            var contentProcessor = contentProcessorProvider.GetProcessor(article.ArticleType);
            article.Content = contentProcessor.ProcessContent(article.Content);
            article.UpdateDate = DateTime.Now;
            article.Tags = await tagService.GetOrCreateTagsAsync(article.Tags.Select(o => o.Name).ToList());
            await articleRep.UpdateAsync(article);
        }

        public async Task DeleteArticleAsync(int id)
        {
            await articleRep.DeleteAsync(id);
            await articleRep.SaveChangesAsync();
        }
    }
}
