using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.ViewComponents
{
    public class Pagination : ViewComponent
    {
        public class PaginationModel
        {
            public int PreLinkCount { get; set; }
            public int NextLinkCount { get; set; }
            public int TotalPage { get; set; }
            public string PageName { get; set; }
            public int PageSize { get; set; }
            public int CurrentIndex { get; set; }
            public int SkipCount { get; set; }
        }
        public async Task<IViewComponentResult> InvokeAsync(string pageName, int pageSize, int total, int skipCount, int preLinkCount = 3, int nextLinkCount = 3)
        {
            return View(new PaginationModel()
            {
                CurrentIndex = skipCount / pageSize + 1,
                PageName = pageName,
                PageSize = pageSize,
                TotalPage = total / pageSize + 1,
                PreLinkCount = preLinkCount,
                NextLinkCount = nextLinkCount,
                SkipCount = skipCount
            });
        }
    }
}
