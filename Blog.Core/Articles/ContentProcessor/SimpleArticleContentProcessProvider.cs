using System;
using System.Collections.Generic;
using System.Text;
using Blog.Core.Articles.Model;

namespace Blog.Core.Articles.ContentProcessor
{
    public class SimpleArticleContentProcessProvider : IArticleContentProcessorProvider
    {
        public IArticleContentProcessor GetProcessor(ArticleType articleType)
        {
            return new SimpleIArticleContentProcessor();
        }
    }
}
