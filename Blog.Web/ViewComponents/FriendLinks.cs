using Blog.Core.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.ViewComponents
{
    public class FriendLinks : ViewComponent
    {
        public FriendLinks(IOptions<BlogOption> option)
        {
            Option = option;
        }

        public IOptions<BlogOption> Option { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(Option);
        }
    }
}
