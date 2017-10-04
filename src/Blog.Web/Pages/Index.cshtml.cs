using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Repository;
using Blog.Core.Articles.Model;
using Blog.Core.Articles;
using Blog.Core.Articles.Dto;
using Blog.Dto;

namespace Blog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private IArticleService articleService;

        [BindProperty(SupportsGet = true)]
        public QueryAriticelInputDto PageQuery { get; set; }
        public PagedResultDto<ArticleDto> Articels { get; private set; }

        public IndexModel(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        public async Task OnGetAsync()
        {
            PageQuery.OnlyPublish = true;
            //HACK 是否要写死每页最大显示?
            PageQuery.MaxResultCount = 10;
            Articels = await articleService.GetArticleByPageAsync(PageQuery);
        }
    }
}
