using Blog.Core.Articles.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Articles.ContentProcessor
{
    public interface IArticleContentProcessorProvider
    {
        IArticleContentProcessor GetProcessor(ArticleType articleType);
    }
}
