using Blog.AutoMapper;
using Blog.Core.Articles.ContentProcessor;
using Blog.Domain.Service;
using Blog.EntityFramework.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Core.Extensions
{
    public static class ServiceCollectionBlogExtensions
    {
        public static IServiceCollection AddBlogService(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddDomainService();
            BlogAutoMapper.Initialization();
            if (!services.Where(o => o.ServiceType == typeof(IArticleContentProcessorProvider)).Any())
            {
                services.AddTransient<IArticleContentProcessorProvider, SimpleArticleContentProcessProvider>();
            }
            return services;
        }
    }
}
