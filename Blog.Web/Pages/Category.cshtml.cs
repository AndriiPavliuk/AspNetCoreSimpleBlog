using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Core.Articles;
using Blog.Core.Articles.Dto;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Pages
{
    public class CategoryModel : PageModel
    {
        private IArticleService _articleService;

        public CategoryModel(IArticleService articleService)
        {
            this._articleService = articleService;
        }

        public IArticleService ArticleService { get; }

        public List<ArticleDto> Articles { get; set; }
        public string CategoryName { get; set; }
        public async Task OnGetAsync(string name)
        {
            CategoryName = name;
            Articles =await _articleService.GetArticleByCategoryAsync(name);
        }
    }
}