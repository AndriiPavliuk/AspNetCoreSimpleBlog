using Blog.AutoMapper;
using Blog.AutoMapper.Attributes;
using Blog.Core.Articles.Model;
using Blog.Core.Categorys.Model;
using Blog.Core.Tags.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Admin.ViewModel
{
    [MapIgnoreNullMember]
    [AutoMap(typeof(Article))]
    public class ArticleViewModel
    {
        public int Id { get; set; }
        [DisplayName("标题")]
        [Required]
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public string Content { set; get; }

        public int ViewCount { set; get; }
        [DefaultValue("无")]
        [DisplayName("摘要")]
        public string Summary { get; set; }
        [DisplayName("文章封面")]
        public string PostImage { get; set; }
        public DateTime UpdateDate { get; set; }
        public ArticleType ArticleType { get; set; }
        public bool IsPublish { get; set; }

        [DisplayName("分类")]
        public Category Category { get; set; }
        [DisplayName("标签")]
        public List<Tag> Tags { get; set; }
        public ArticleViewModel()
        {

        }
    }

}
