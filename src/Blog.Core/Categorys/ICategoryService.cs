using Blog.Core.Categorys.Dto;
using Blog.Core.Categorys.Model;
using Blog.Domain.Service;
using Blog.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Categorys
{
    public interface ICategoryService : IDomainService
    {
        Task<PagedResultDto<CategoryDto>> GetCategoryByPageAsync(QueryCatogoryInputDto pagedResult);
        Task DeleteCategoryAsync(int id);
        Task<Category> AddCategoryAsync(string name);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<Category> GetCategoryAsync(int id);
    }
}
