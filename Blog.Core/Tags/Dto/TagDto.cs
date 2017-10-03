using Blog.AutoMapper;
using Blog.Core.Tags.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Tags.Dto
{
    [AutoMap(typeof(Tag))]
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArticleCount { get; set; }
    }
}
