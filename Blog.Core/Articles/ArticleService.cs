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
using Blog.AutoMapper;

namespace Blog.Core.Articles
{
    public class ArticleService : IArticleService
    {
        private IRepository<Article> _articleRep;
        private ITagService _tagService;
        private IArticleContentProcessorProvider _contentProcessorProvider;

        public ArticleService(
            IRepository<Article> articleRep,
            ITagService tagService,
            IArticleContentProcessorProvider contentProcessorProvider)
        {
            this._articleRep = articleRep;
            this._tagService = tagService;
            this._contentProcessorProvider = contentProcessorProvider;
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
            var result = await _articleRep.InsertAsync(newArticle);
            await _articleRep.SaveChangesAsync();
            return result;
        }

        public async Task<Article> GetArticelAsync(int id)
        {
            var article = await _articleRep.GetAllIncluding(o => o.Tags).Where(o => o.Id == id).FirstAsync();

            if (article.Tags == null)
            {
                article.Tags = new List<Tag>();
            }
            await _articleRep.SaveChangesAsync();

            return article;
        }

        public async Task<PagedResultDto<ArticleDto>> GetArticleByPageAsync(QueryAriticelInputDto pageQuery)
        {
            var query = _articleRep.GetAll();
            if (pageQuery.OnlyPublish)
            {
                query = query.Where(o => o.IsPublish == true);
            }
            if (pageQuery.WithTags)
            {
                query = query.Include(o => o.Tags);
            }
            if (pageQuery.WithCategory)
            {
                query = query.Include(o => o.Category);
            }

            var resultList = await query
                .OrderBy(pageQuery.Sorting ?? $"{nameof(Article.PostDate)} DESC")
                .Skip(pageQuery.SkipCount)
                .Take(pageQuery.MaxResultCount)
                .ToListAsync();
            var total = await query.CountAsync();
            return new PagedResultDto<ArticleDto>(total, resultList.MapTo<List<ArticleDto>>());
        }

        public async Task<List<Article>> GetArticelByTag(string tagName)
        {
            return await _articleRep.GetAll().Include(o => o.Tags).Where(o => o.Tags.Where(t => t.Name == tagName).Any()).ToListAsync();
        }
        public async Task UpdateArticleAsync(Article article)
        {
            var contentProcessor = _contentProcessorProvider.GetProcessor(article.ArticleType);
            article.Content = contentProcessor.ProcessContent(article.Content);
            article.UpdateDate = DateTime.Now;
            article.Tags = await _tagService.GetOrCreateTagsAsync(article.Tags.Select(o => o.Name).ToList());
            await _articleRep.UpdateAsync(article);
        }

        public async Task DeleteArticleAsync(int id)
        {
            await _articleRep.DeleteAsync(id);
            await _articleRep.SaveChangesAsync();
        }
    }
}
