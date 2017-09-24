using Blog.Core.Articles.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core
{
    public class BlogDbContext:DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public BlogDbContext(DbContextOptions<BlogDbContext> options):base(options)
        {

        }
    }
}
