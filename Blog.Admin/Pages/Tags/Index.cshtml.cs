using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Core.Tags;
using Blog.Admin.ViewModel;
using Blog.Core.Tags.Dto;

namespace Blog.Admin.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private ITagService _tagService;

        public IndexModel(ITagService tagService)
        {
            this._tagService = tagService;
        }
        public void OnGet()
        {

        }
        [BindProperty(SupportsGet = true)]
        public BootStrapTableQueryModel Query { get; set; }


        public async Task<IActionResult> OnGetLoadTagAsync()
        {
            var queryInput = new QueryTagInputDto();
            queryInput.FetchFromOther(Query.ToDto());

            var result =(await _tagService.GetTagByPageAsync(queryInput)).ToBootStrapQueryResultModel();
            //var result = (await _categoryService.GetCategoryByPageAsync(queryInput)).ToBootStrapQueryResultModel();

            return new JsonResult(result);

        }
        public async Task<IActionResult> OnDeleteAsync(int id)
        {
            await _tagService.DeleteTagAsync(id);
            return base.StatusCode(204);
        }
    }
}