using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Admin.ViewModel;
using Blog.Core.Articles;
using Blog.Core.Articles.Dto;
using Blog.Core.Tags;
using AutoMapper;
using Blog.Core.Articles.Model;

namespace Blog.Admin.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private IArticleService articleService;
        private ITagService tagService;

        public IndexModel(IArticleService articleService, ITagService tagService)
        {
            this.articleService = articleService;
            this.tagService = tagService;
        }
        public void OnGet()
        {
        }

        [BindProperty(SupportsGet = true)]
        public BootStrapTableQueryModel Query { get; set; }
        public async Task<IActionResult> OnGetLoadArticleAsync()
        {
            var queryInput = new QueryAriticelInputDto();
            queryInput.FetchFromOther(Query.ToDto());
            queryInput.OnlyPublish = false;
            queryInput.WithTags = true;
            queryInput.WithCategory = true;

            var result = (await articleService.GetArticleByPageAsync(queryInput)).ToBootStrapQueryResultModel();

            return new JsonResult(result);
        }

        public async Task<IActionResult> OnGetNewArticleAsync(string articleType)
        {
            if (articleType.Equals("Markdown", StringComparison.InvariantCultureIgnoreCase))
            {
                var newArticle = await articleService.AddArticleAsync(new Article() {  ArticleType= ArticleType.MarkDown});
                return RedirectToPage($"/Articles/Edit", new { articleId = newArticle.Id });
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public async Task<IActionResult> OnGetTogglePublishAsync(int id)
        {
            var article = await articleService.GetArticleAsync(id);
            article.IsPublish = !article.IsPublish;
            await articleService.UpdateArticleAsync(article);
            return StatusCode(201);
        }

        public async Task OnDeleteAsync(int id)
        {
            await articleService.DeleteArticleAsync(id);
        }
    }
}