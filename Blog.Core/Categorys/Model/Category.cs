using Blog.Core.Articles.Model;
using Blog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Categorys.Model
{
    public class Category:Entity
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public ICollection<Article> Articles{ get; set; }
    }
}
