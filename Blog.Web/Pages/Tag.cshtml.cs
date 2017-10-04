using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Core.Articles.Model;
using Blog.Core.Articles;
using Blog.Core.Articles.Dto;

namespace Blog.Web.Pages
{
    public class TagModel : PageModel
    {
        private IArticleService articleService;

        public string TagName { get; set; }
        public List<ArticleDto> Articles { get; private set; }

        public TagModel(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        public async Task OnGetAsync(string Tag)
        {
            TagName = Tag;
            this.Articles = await  articleService.GetArticleByTag(Tag);
        }
    }
}