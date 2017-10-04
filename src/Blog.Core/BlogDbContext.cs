using Blog.Core.Articles.Model;
using Blog.Core.Categorys.Model;
using Blog.Core.Tags.Model;
using Blog.Core.Extensions;
using Blog.Core.Users.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Core.Relationship;

namespace Blog.Core
{
    public class BlogDbContext : IdentityDbContext<User>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ChangeTablePrefix<IdentityRole,User>("");
            //modelBuilder.Entity<Tag>().HasMany(o=>o.Articles).
            
        }
    }
}
