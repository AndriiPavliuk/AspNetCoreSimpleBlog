using Blog.Core.Articles.Model;
using Blog.Core.Categorys.Model;
using Blog.Core.Relationship;
using Blog.Core.Tags.Model;
using Blog.Core.Users.Model;
using Blog.Threading;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Core
{
    public class BlogDbInitializer
    {
        public BlogDbInitializer()
        {

        }
        public BlogDbInitializer(BlogDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public BlogDbContext DbContext { get; }

        public static void Initialize(BlogDbContext dbContext)
        {
            var instance = new BlogDbInitializer(dbContext);
            instance.SeedArticle();
            dbContext.SaveChanges();
        }

        public BlogDbInitializer SeedArticle()
        {
            if (!DbContext.Tags.Any())
            {
                this.SeedTags();
            }
            if (!DbContext.Categorys.Any())
            {
                this.SeedCategory();
            }
            if (DbContext.Articles.Any())
            {
                return this;
            }
            var tags = DbContext.Tags.ToList();
            var categorys = DbContext.Categorys.ToList();

            for (var i = 0; i < tags.Count; i++)
            {
                var newArticle = new Article()
                {
                    Summary = "摘要" + i,
                    Content = "内容" + i,
                    PostDate = DateTime.Now,
                    Title = "标题" + i,
                    ViewCount = i,
                    IsPublish = i % 2 == 0 ? true : false,
                    ArticleType = ArticleType.MarkDown,
                    UpdateDate=DateTime.Now,
                };
                newArticle.ArticleTags = newArticle.ArticleTags == null ? new List<ArticleTag>() : newArticle.ArticleTags;
                newArticle.ArticleTags.Add(new ArticleTag()
                {
                    ArticleId = newArticle.Id,
                    TagId = tags.ElementAt(i).Id
                });
                newArticle.Category = categorys.ElementAt(i % categorys.Count);
                DbContext.Articles.Add(newArticle);
            }
            DbContext.SaveChanges();

            return this;
        }

        public BlogDbInitializer SeedCategory()
        {
            if (DbContext.Categorys.Any())
            {
                return this;
            }
            for (var i = 0; i < 10; i++)
            {
                DbContext.Categorys.Add(new Category()
                {
                    Name = "分类" + i.ToString(),
                    CreateTime = DateTime.Now
                });
            }
            DbContext.SaveChanges();
            return this;
        }

        public BlogDbInitializer SeedTags()
        {
            if (DbContext.Tags.Any())
            {
                return this;
            }
            for (var i = 0; i < 20; i++)
            {
                DbContext.Tags.Add(new Tag()
                {
                    Name = "标签" + i.ToString()
                });
            }
            DbContext.SaveChanges();
            return this;
        }

        public BlogDbInitializer SeedUser(UserManager<User> userManager)
        {

            //Admin role for host
            //创建默认Admin角色
            var adminRole = DbContext.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (adminRole == null)
            {
                adminRole = DbContext.Roles.Add(new IdentityRole { Name = "Admin" }).Entity;
                DbContext.SaveChanges();
            }

            //Admin user for tenancy host
            var adminUser = DbContext.Users.FirstOrDefault(u => u.UserName == User.AdminEmail);
            if (adminUser == null && DbContext.Users.Count() == 0)
            {
                var newUser = new User
                {
                    UserName = User.AdminEmail,
                    //NormalizedUserName = User.AdminEmail.ToUpper(),
                    PhoneNumber = "18300000000",
                    Email = User.AdminEmail,
                    EmailConfirmed = true,
                    //NormalizedEmail = User.AdminEmail.ToUpper(),
                };
                var result = AsyncHelper.RunSync(async () => await userManager.CreateAsync(newUser, User.DefaultPsw));
                if (!result.Succeeded)
                {
                    var error = "";
                    foreach (var item in result.Errors)
                    {
                        error += item.Description;
                    }
                    throw new Exception(error);
                }
                DbContext.SaveChanges();

                var userRole = new IdentityUserRole<string>()
                {
                    RoleId = adminRole.Id,
                    UserId = newUser.Id
                };
                DbContext.UserRoles.Add(userRole);

                DbContext.SaveChanges();
            }
            return this;
        }

        //private bool HasSeedTags()
        //{
        //    if (DbContext.Tags.Any())
        //    {

        //    }
        //}
    }
}
