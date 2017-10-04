using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Mvc
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JsResourceAttribute : Attribute
    {
        public JsResourceAttribute(params string[] jsFilePaths)
        {
            this.JsFilePaths = jsFilePaths;
        }

        public string[] JsFilePaths { get; }
    }
}
