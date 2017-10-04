using Blog.Core.Categorys;
using Blog.Core.Categorys.Dto;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blog.Test.Categorys
{
    public class CategoryService_Test : BlogTestBase
    {
        private ICategoryService _categoryService;

        public CategoryService_Test()
        {
            this.InitDb();
            this._categoryService = _serviceProvider.GetRequiredService<ICategoryService>();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCategoryByPage_TestAsync()
        {
            var result=await _categoryService.GetCategoryByPageAsync(new QueryCatogoryInputDto()
            {
                SkipCount = 1,
                MaxResultCount = 5,
            });
            foreach (var item in result.Items)
            {
                item.ArticleCount.ShouldBeGreaterThanOrEqualTo(1);
            }

        }
    }
}
