using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Repository;
using Blog.Core.Articles.Model;

namespace Blog.Web.Pages
{
    public class ArchivesModel : PageModel
    {
        private IRepository<Article> articleRep;

        public ArchivesModel(IRepository<Article> articleRep)
        {
            this.articleRep = articleRep;
        }

        public IEnumerable<IGrouping<DateTime, Article>> GroupedArticles { get; private set; }

        public void OnGet()
        {
            GroupedArticles = articleRep
                .GetAll()
                .OrderByDescending(o => o.PostDate)
                .ToList()
                .Select(o => new
                {
                    PostMonth = new DateTime(o.PostDate.Year, o.PostDate.Month, 1),
                    Article = o
                }).GroupBy(o => o.PostMonth,o=>o.Article);
        }
    }
}