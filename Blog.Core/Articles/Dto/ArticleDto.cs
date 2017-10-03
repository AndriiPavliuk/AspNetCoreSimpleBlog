using AutoMapper;
using Blog.AutoMapper;
using Blog.AutoMapper.Attributes;
using Blog.Core.Articles.Model;
using Blog.Core.Categorys.Dto;
using Blog.Core.Tags.Dto;
using Blog.Core.Tags.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Articles.Dto
{
    [AutoMap(typeof(Article))]
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public string Content { set; get; }

        public int ViewCount { set; get; }
        public string Summary { get; set; }
        public string PostImage { get; set; }
        public DateTime UpdateDate { get; set; }
        public ArticleType ArticleType { get; set; }
        public bool IsPublish { get; set; }
        [DontMapTo(typeof(Article))]
        public CategoryDto Category { get; set; }
        [DontMapTo(typeof(Article))]
        public ICollection<TagDto> Tags { get; set; }
    }
}
