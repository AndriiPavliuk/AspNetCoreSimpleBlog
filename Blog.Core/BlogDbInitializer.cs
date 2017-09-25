using Blog.Core.Articles.Model;
using Blog.Core.Categorys.Model;
using Blog.Core.Tags.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Core
{
    public class BlogDbInitializer
    {
        public BlogDbInitializer()
        {

        }
        public BlogDbInitializer(BlogDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public BlogDbContext DbContext { get; }

        public static void Initialize(BlogDbContext dbContext)
        {
            var instance = new BlogDbInitializer(dbContext);
            instance.SeedArticle();
        }

        public BlogDbInitializer SeedArticle()
        {
            if (!DbContext.Tags.Any())
            {
                this.SeedTags();
            }
            if (!DbContext.Categorys.Any())
            {
                this.SeedCategory();
            }
            if (DbContext.Articles.Any())
            {
                return this;
            }
            var tags = DbContext.Tags.ToList();
            var categorys = DbContext.Categorys.ToList();

            for (var i = 0; i < tags.Count; i++)
            {
                var newArticle = new Article()
                {
                    Summary = "摘要" + i,
                    Content = "内容" + i,
                    PostDate = DateTime.Now,
                    Title = "标题" + i,
                    ViewCount = i,
                    IsPublish = i % 2 == 0 ? true : false
                };
                newArticle.Tags = newArticle.Tags == null ? new List<Tag>() : newArticle.Tags;
                newArticle.Tags.Add(tags.ElementAt(i));
                newArticle.Category = categorys.ElementAt(i % categorys.Count);
                DbContext.Articles.Add(newArticle);
            }
            DbContext.SaveChanges();

            return this;
        }

        public BlogDbInitializer SeedCategory()
        {
            if (DbContext.Categorys.Any())
            {
                return this;
            }
            for (var i = 0; i < 10; i++)
            {
                DbContext.Categorys.Add(new Category()
                {
                    Name = "分类" + i.ToString()
                });
            }
            DbContext.SaveChanges();
            return this;
        }

        public BlogDbInitializer SeedTags()
        {
            if (DbContext.Tags.Any())
            {
                return this;
            }
            for (var i = 0; i < 20; i++)
            {
                DbContext.Tags.Add(new Tag()
                {
                    Name = "标签" + i.ToString()
                });
            }
            DbContext.SaveChanges();
            return this;
        }

        //private bool HasSeedTags()
        //{
        //    if (DbContext.Tags.Any())
        //    {

        //    }
        //}
    }
}
