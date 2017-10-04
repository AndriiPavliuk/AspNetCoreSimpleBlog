using Blog.Admin.ViewModel;
using Blog.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Admin.ViewComponents
{
    [JsResource("/js/plugins/editorMd/editormd.js", "/js/article/editormd_editor.js")]
    [CssResource("/css/plugins/editorMd/editormd.min.css")]
    public class EditorMdEditor: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ArticleViewModel model)
        {
            return View(model);
        }
    }
}
