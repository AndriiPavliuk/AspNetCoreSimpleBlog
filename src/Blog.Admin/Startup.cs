using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.Admin.Services;
using Blog.Core;
using Blog.Core.Users.Model;
using Blog.EntityFramework.Repository;
using Blog.Domain.Service;
using Blog.Core.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Blog.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddDbContext<BlogDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddDbContext<BlogDbContext>(options =>
               options.UseSqlite(Configuration.GetConnectionString("Default")));
            services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<BlogDbContext>()
            .AddDefaultTokenProviders();
            services.AddAntiforgery(o =>
            {
                o.HeaderName = "X-CSRF-TOKEN";
            });
            services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });

            SeedUser(services);
            services.AddBlogService();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            //services.AddSingleton<IEmailSender, EmailSender>();
        }

        private void SeedUser(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetService<BlogDbContext>();
            if ((dbContext.Database.GetService<IDatabaseCreator>() 
                as RelationalDatabaseCreator).Exists())
            {
                var dbInit = new BlogDbInitializer(dbContext);
                dbInit.SeedUser(serviceProvider.GetService<UserManager<User>>());
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
