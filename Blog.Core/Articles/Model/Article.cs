using Blog.Core.Categorys.Model;
using Blog.Core.Relationship;
using Blog.Core.Tags.Model;
using Blog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime PostDate { get; set; }
        [Required]
        public string Content { set; get; }

        public int ViewCount { set; get; }
        [Required]
        public string Summary { get; set; }
        public string PostImage { get; set; }
        public DateTime UpdateDate { get; set; }
        public ArticleType ArticleType { get; set; }
        public bool IsPublish { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
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
