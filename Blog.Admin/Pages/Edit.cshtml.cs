using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Core.Articles;
using Blog.Core.Articles.Model;

namespace Blog.Admin.Pages
{
    public class EditModel : PageModel
    {
        private IArticleService articleService;

        public EditModel(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public Article CurrentArticle { get; private set; }

        public async Task OnGetAsync(int articleId)
        {
            CurrentArticle=await articleService.GetArticelAsync(articleId);
        }
    }
}