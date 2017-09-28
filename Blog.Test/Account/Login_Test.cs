using Blog.Core;
using Blog.Core.Users.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blog.Test.Account
{
    public class Login_Test : BlogTestBase
    {
        private UserManager<User> userManager;
        private UserStore<User> userStore;

        public Login_Test()
        {
            AddTestDbContext<BlogDbContext>();
            BlogDbInitializer.Initialize(base.DbContext as BlogDbContext);
            userManager = _serviceProvider.GetService<UserManager<User>>();
            userStore = new UserStore<User>(base.DbContext as BlogDbContext);
        }
        protected override void AdditionService(ServiceCollection serviceCollection)
        {
            base.AdditionService(serviceCollection);
            serviceCollection.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();
        }

        [Fact]
        public async System.Threading.Tasks.Task LoginTestAsync()
        {
          
            var user =await userManager.FindByNameAsync(User.AdminEmail);
            user.ShouldNotBeNull();
            var checkResult=await userManager.CheckPasswordAsync(user, User.DefaultPsw);
            checkResult.ShouldBeTrue();
        }

    }
}
