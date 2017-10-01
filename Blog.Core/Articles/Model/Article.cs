using Blog.Core.Categorys.Model;
using Blog.Core.Tags.Model;
using Blog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Core.Articles.Model
{
    public enum ArticleType
    {
        MarkDown = 1,
        HTML = 2,
    }
    public class Article : Entity
    {
        [Display(Description = "标题")]
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime PostDate { get; set; }
        [Required]
        public string Content { set; get; }

        public int ViewCount { set; get; }
        [Required]
        public string Summary { get; set; }
        [Display(Description = "文章封面")]
        public string PostImage { get; set; }
        public DateTime UpdateDate { get; set; }
        public ArticleType ArticleType { get; set; }
        public bool IsPublish { get; set; }



        public Category Category { get; set; }
        [Display(Description = "标签")]
        public ICollection<Tag> Tags { get; set; }
        public Article()
        {

        }
        public Article(string title, string content, string summary = "", string postImage = "", ArticleType articleType = ArticleType.MarkDown)
        {
            if (summary.IsNullOrWhiteSpace())
            {
                this.Summary = content.Length > 150 ? content.Substring(0, 150) : content;
            }
        }
    }
}
