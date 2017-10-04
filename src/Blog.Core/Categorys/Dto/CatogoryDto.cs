using Blog.AutoMapper;
using Blog.Core.Categorys.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Categorys.Dto
{
    [AutoMap(typeof(Category))]
    public class CategoryDto
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; }
        public int ArticleCount { get; set; }
    }
}
