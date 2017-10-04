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
using Blog.Core.Articles.Dto;
using Blog.AutoMapper;

namespace Blog.Web.Pages
{
    public class PostModel : PageModel
    {
        private IArticleService articleService;

        public PostModel(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public ArticleDto Article { get; private set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Article = await articleService.GetArticleAsync(id);
                Article.ViewCount += 1;
                await articleService.UpdateArticleAsync(Article);
            }
            catch (EntityNotFoundException e)
            {
                return new NotFoundResult();
            }
            return Page();
        }
    }
}