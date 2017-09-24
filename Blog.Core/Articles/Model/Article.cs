using Blog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Articles.Model
{
    public class Article : Entity
    {
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public string Content { set; get; }
        public int ViewCount { set; get; }
       
    }
}
