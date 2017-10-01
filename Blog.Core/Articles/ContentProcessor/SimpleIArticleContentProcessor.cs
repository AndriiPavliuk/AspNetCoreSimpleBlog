using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Articles.ContentProcessor
{
    public class SimpleIArticleContentProcessor : IArticleContentProcessor
    {
        public string ProcessContent(string content)
        {
            return content;
        }
    }
}
