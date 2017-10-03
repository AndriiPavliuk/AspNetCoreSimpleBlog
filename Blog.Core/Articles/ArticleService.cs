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
using Blog.Core.Tags.Dto;
using Blog.Core.Relationship;

namespace Blog.Core.Articles
{
    public class ArticleService : IArticleService
    {
        private IRepository<Article> _articleRep;
        private ITagService _tagService;
        private IArticleContentProcessorProvider _contentProcessorProvider;
        private IRepository<ArticleTag> _articleTagRep;

        public ArticleService(
            IRepository<Article> articleRep,
            ITagService tagService,
            IRepository<ArticleTag> articleTagRep,
            IArticleContentProcessorProvider contentProcessorProvider)
        {
            this._articleRep = articleRep;
            this._tagService = tagService;
            this._contentProcessorProvider = contentProcessorProvider;
            this._articleTagRep = articleTagRep;
        }

        public async Task<ArticleDto> AddArticleAsync(Article newArticle)
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
            return result.MapTo<ArticleDto>();
        }

        public async Task<ArticleDto> GetArticelAsync(int id)
        {
            //TODO Article Not Found
            var article = await _articleRep
                .GetAll()
                .Where(o => o.Id == id)
                .Include(o=>o.Category)
                .Include(o=>o.ArticleTags)
                .ThenInclude(a=>a.Tag)
                .FirstAsync();

            
            var result = article.MapTo<ArticleDto>();
            result.Tags = article.ArticleTags.Select(o => o.Tag.MapTo<TagDto>()).ToList();
            return result;
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
                query = query.Include(o => o.ArticleTags);
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
            List<ArticleDto> result;
            if (pageQuery.WithTags)
            {
                result = resultList.Select(o =>
                  {
                      var tmp = o.MapTo<ArticleDto>();
                      tmp.Tags = o.ArticleTags.Select(at => at.Tag.MapTo<TagDto>()).ToList();
                      return tmp;
                  }).ToList();
            }
            else
            {
                result = resultList.MapTo<List<ArticleDto>>();
            }
            var total = await query.CountAsync();
            return new PagedResultDto<ArticleDto>(total, result);
        }

        public async Task<List<ArticleDto>> GetArticelByTag(string tagName)
        {
            var result = (await _articleRep.GetAll()
                .Include(o => o.ArticleTags)
                .Where(o => o.ArticleTags.Where(t => t.Tag.Name == tagName).Any())
                .ToListAsync())
                .Select(o =>
                {
                    var tmp = o.MapTo<ArticleDto>();
                    tmp.Tags = o.ArticleTags.Select(at => at.Tag.MapTo<TagDto>()).ToList();
                    return tmp;
                }).ToList();
            return result;
        }
        //TODO Test this
        public async Task UpdateArticleAsync(ArticleDto articledto)
        {
            
            var contentProcessor = _contentProcessorProvider.GetProcessor(articledto.ArticleType);
            var article = await _articleRep.GetAsync(articledto.Id);
            articledto.MapTo(article);
            article.Content= contentProcessor.ProcessContent(articledto.Content);
            article.UpdateDate = DateTime.Now;
            //改CategoryId而不是Category
            article.CategoryId = articledto.Category?.Id;
            if (article.CategoryId == 0)
            {
                article.CategoryId = null;
            }

            await _articleRep.UpdateAsync(article);
            await _articleRep.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(int id)
        {
            await _articleRep.DeleteAsync(id);
            await _articleRep.SaveChangesAsync();
        }

        public async Task UpdateArticleTagsAsync(int id, IList<TagDto> inputTags)
        {
            var article =await _articleRep
                         .GetAllIncluding(o=>o.ArticleTags)
                         .Where(o=>o.Id==id).FirstAsync();
            var exsistTags = article.ArticleTags.Select(o => o.TagId);
            var inputTagEntities=await _tagService.GetOrCreateTagsAsync(inputTags?.Select(o => o.Name).ToList());

            var newTags=inputTagEntities.Where(t => !exsistTags.Contains(t.Id)).ToList();
            var deleteingTags = exsistTags.Where(o => !inputTagEntities.Select(t => t.Id).Contains(o));

            await _articleTagRep.DeleteAsync(o => o.ArticleId == id && deleteingTags.Contains(o.TagId));
            foreach (var item in newTags)
            {
                _articleTagRep.Insert(new ArticleTag()
                {
                     ArticleId=id,
                     TagId=item.Id
                });
            }
            await _articleRep.SaveChangesAsync();
        }
    }
}
