using Blog.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.Domain.Service
{
    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }
            ITypeFinder finder = new TypeFinder();
            var implementationTypes = finder.Find(a =>
            {
                var b = a.IsAbstract == false
                       && a.IsClass
                       && typeof(IDomainService).IsAssignableFrom(a);
                return b;
            });
            RegisterDomainService(services, implementationTypes);
            return services;
        }

        static void RegisterDomainService(IServiceCollection services, ICollection<Type> implementationTypes)
        {
            //implementationTypes是类的Types集合
            Type domainServiceType = typeof(IDomainService);
            foreach (Type implementationType in implementationTypes)
            {
                //一般自己写个继承IDomainService的接口,在写个类实现它

                //找到这个类实现的非IDomainService接口(并且该接口继承了IDomainService)
                var implementedAppServiceTypes = implementationType
                                                    .GetTypeInfo()
                                                    .ImplementedInterfaces
                                                    .Where(a => a != domainServiceType && domainServiceType.IsAssignableFrom(a));
                //注册类与非IDomainService接口
                foreach (Type implementedAppServiceType in implementedAppServiceTypes)
                {
                    if (typeof(IDisposable).IsAssignableFrom(implementationType))
                        services.AddScoped(implementedAppServiceType, implementationType);
                    else
                        services.AddTransient(implementedAppServiceType, implementationType);
                }
            }
        }
    }
}
