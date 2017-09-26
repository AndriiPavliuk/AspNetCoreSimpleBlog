using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Repository;
using Blog.Core.Articles.Model;
using Blog.Core.Articles;
using Blog.EntityFramework.Exceptions;

namespace Blog.Web.Pages
{
    public class PostModel : PageModel
    {
        private IArticleService articleService;

        public PostModel(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public Article Article { get; private set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Article = await articleService.GetArticelAsync(id);
            }
            catch (EntityNotFoundException e)
            {
                return new NotFoundResult();
            }
            return Page();
        }
    }
}