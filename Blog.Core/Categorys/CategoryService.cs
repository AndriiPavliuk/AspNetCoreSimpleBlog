using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Categorys.Dto;
using Blog.Dto;
using Blog.Repository;
using Blog.Core.Categorys.Model;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Blog.Core.Articles.Model;
using Blog.EntityFramework.Repository;

namespace Blog.Core.Categorys
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> _categoryRep;
        private IRepository<Article> _articleRep;

        public CategoryService(IRepository<Category> categoryRep, IRepository<Article> articleRep)
        {
            this._categoryRep = categoryRep;
            this._articleRep = articleRep;
        }

        public async Task<Category> AddCategoryAsync(string name)
        {
            var newCategory = new Category()
            {
                Name = name,
                CreateTime = DateTime.Now
            };
            await this._categoryRep.InsertAsync(newCategory);
            await _categoryRep.SaveChangesAsync();
            return newCategory;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRep.DeleteAsync(id);
            await _categoryRep.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _categoryRep.GetAsync(id);
        }

        public async Task<PagedResultDto<CategoryDto>> GetCategoryByPageAsync(QueryCatogoryInputDto pagedQuery)
        {
            var query = _categoryRep.GetAll();
            var resultList = await query
             .OrderBy(pagedQuery.Sorting ?? $"{nameof(Category.CreateTime)} DESC")
             .Skip(pagedQuery.SkipCount)
             .Take(pagedQuery.MaxResultCount)
             .Select(c => new CategoryDto()
             {
                 Id=c.Id,
                 Name = c.Name,
                 ArticleCount = c.Articles.Count,
                 CreateTime = c.CreateTime
             })
             .ToListAsync();
            var total = await query.CountAsync();
            return new PagedResultDto<CategoryDto>(total, resultList);
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var result=await _categoryRep.UpdateAsync(category);
            await _categoryRep.SaveChangesAsync();
            return result;
        }
    }
}
