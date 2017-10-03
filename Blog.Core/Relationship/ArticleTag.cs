using Blog.Core.Articles.Model;
using Blog.Core.Tags.Model;
using Blog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Relationship
{
    public class ArticleTag : Entity
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

    }
}
