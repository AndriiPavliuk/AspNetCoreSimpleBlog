using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Articles.ContentProcessor
{
    public interface IArticleContentProcessor
    {
        string ProcessContent(string content);
    }
}
