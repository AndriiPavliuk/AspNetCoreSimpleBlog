using Blog.Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blog.Test.Infrastructure
{
    public interface IFooSerice: IDomainService
    {
        string GetMyName();
    }
    public class FooService : IFooSerice
    {
        public string GetMyName()
        {
            return nameof(FooService);
        }
    }

    public class DomainService_Test
    {
        [Fact]
        public void AddServices_Test()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDomainService();
            var provider = services.BuildServiceProvider();
            var instance=provider.GetService<IFooSerice>();

            instance.ShouldNotBeNull();
            instance.GetMyName().ShouldBe(nameof(FooService));
        }
    }
}
