using Blog.Admin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Mvc;
namespace Blog.Admin.ViewComponents
{
    /*
     <script src="/js/plugins/pen/pen.js"></script>
    <script src="/js/plugins/pen/pen-markdown.js"></script>
    <script src="/js/article/editor.js"></script>
        <link href="/js/plugins/pen/pen.css" rel="stylesheet" />
    <link href="/css/article/pen_editor.css" rel="stylesheet" />
         */
    [CssResource("/js/plugins/pen/pen.css", "/css/article/pen_editor.css")]
    [JsResource("/js/plugins/pen/pen.js", "/js/plugins/pen/pen-markdown.js", "/js/article/pen_editor.js")]
    public class PenEditor : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ArticleViewModel model)
        {
            return View(model);
        }
    }
}
