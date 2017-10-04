using Blog.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class VCRenderResourceExtension
    {
        public static IHtmlContent RenderJsResource(this IHtmlHelper htmlHelper,Type ViewComponetType)
        {
            if (!ViewComponetType.IsDefined(typeof(JsResourceAttribute)))
            {
                return new HtmlString(""); 
            }
            var attribute=ViewComponetType.GetCustomAttribute<JsResourceAttribute>();
            var result = new StringBuilder();
            foreach (var item in attribute.JsFilePaths)
            {
                result.AppendLine($"<script src=\"{item}\" /></script>");
            }
            return new HtmlString(result.ToString());
        }
        public static IHtmlContent RenderCssResource(this IHtmlHelper htmlHelper,Type ViewComponetType)
        {
            if (!ViewComponetType.IsDefined(typeof(CssResourceAttribute)))
            {
                return new HtmlString("");
            }
            var attribute = ViewComponetType.GetCustomAttribute<CssResourceAttribute>();
            var result = new StringBuilder();
            foreach (var item in attribute.CssPath)
            {
                result.AppendLine($"<link rel=\"stylesheet\"  href=\"{item}\" />");
            }
            return new HtmlString(result.ToString());
        }
    }
}
