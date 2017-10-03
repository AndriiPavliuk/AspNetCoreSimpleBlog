using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Email
{
    public static class ServiceCollectionEmailExtensions
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            return services.AddTransient<IEmailSender, EmailSender>();
        }
    }
}
