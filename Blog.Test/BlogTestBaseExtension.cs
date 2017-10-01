using Blog.Core;
using Blog.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Test
{
    public static class BlogTestBaseExtension
    {
        public static void InitDb(this BlogTestBase blogTest)
        {
            blogTest.AddTestDbContext<BlogDbContext>();
            BlogDbInitializer.Initialize(blogTest.DbContext as BlogDbContext);
        }
    }
}
