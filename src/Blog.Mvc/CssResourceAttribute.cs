using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Mvc
{
    [AttributeUsage( AttributeTargets.Class)]
    public class CssResourceAttribute:Attribute
    {
        public CssResourceAttribute(params string [] cssPaths)
        {
            this.CssPath = cssPaths;
        }

        public string[] CssPath { get; }
    }
}
