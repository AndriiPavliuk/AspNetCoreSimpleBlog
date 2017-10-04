using Blog.Core.Articles.Model;
using Blog.Core.Relationship;
using Blog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Tags.Model
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
