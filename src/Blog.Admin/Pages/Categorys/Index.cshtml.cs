using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Core.Categorys;
using Blog.Core.Categorys.Dto;
using Blog.Admin.ViewModel;
using Blog.Repository;
using Blog.Core.Categorys.Model;
using System.Net;

namespace Blog.Admin.Pages.Categorys
{
    public class IndexModel : PageModel
    {
        private ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        [BindProperty(SupportsGet = true)]
        public BootStrapTableQueryModel Query { get; set; }
        public async Task<IActionResult> OnGetLoadCategoryAsync()
        {
            var queryInput = new QueryCatogoryInputDto();
            queryInput.FetchFromOther(Query.ToDto());

            var result = (await _categoryService.GetCategoryByPageAsync(queryInput)).ToBootStrapQueryResultModel();

            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPost(string name)
        {
            await _categoryService.AddCategoryAsync(name);
            return base.StatusCode(201);
        }

        public async Task<IActionResult> OnPut(string name,int id)
        {
            var category =await _categoryService.GetCategoryAsync(id);
            category.Name = name;
            await _categoryService.UpdateCategoryAsync(category);
            return base.StatusCode(204);
        }

        public async Task<IActionResult> OnDelete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return base.StatusCode(204);
        }

        public void OnGet()
        {
        }
    }
}