using Blog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Admin.ViewModel
{
    public class BootStrapQueryResultModel<T>
    {
        public int total { get; set; }
        public IReadOnlyList<T> rows { get; set; }
    }

    public static class PagedResultDtoExtension
    {
        public static BootStrapQueryResultModel<T> ToBootStrapQueryResultModel<T>(this PagedResultDto<T> dto)
        {
            var result = new BootStrapQueryResultModel<T>();
            result.total = dto.TotalCount;
            result.rows = dto.Items;
            return result;
        }
    }
}
