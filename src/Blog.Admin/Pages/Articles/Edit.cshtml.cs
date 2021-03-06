using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Core.Articles;
using Blog.Core.Articles.Model;
using Blog.Admin.ViewModel;
using Blog.AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Blog.Admin.Pages.Articles
{
    public class EditModel : PageModel
    {
        private IArticleService articleService;
        private ILogger<EditModel> logger;

        public EditModel(IArticleService articleService, ILogger<EditModel> logger)
        {
            this.articleService = articleService;
            this.logger = logger;
        }

        [BindProperty]
        public ArticleViewModel CurrentArticle { get; set; }

        public async Task OnGetAsync(int articleId)
        {
            var article = await articleService.GetArticleAsync(articleId);
            CurrentArticle = article.MapTo<ArticleViewModel>();

        }
        public async Task<ActionResult> OnPutAsync()
        {
            var article =await articleService.GetArticleAsync(CurrentArticle.Id);
            var editedArticle = CurrentArticle.MapTo(article);
            await articleService.UpdateArticleAsync(editedArticle);
            await articleService.UpdateArticleTagsAsync(editedArticle.Id, CurrentArticle.Tags);
            return StatusCode(204);
        }
        
    }
}