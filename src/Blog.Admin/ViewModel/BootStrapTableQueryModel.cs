using Blog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Admin.ViewModel
{
    public class BootStrapTableQueryModel
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public string sort { get; set; }

        public PagedResultRequestDto ToDto()
        {
            return new PagedResultRequestDto()
            {
                MaxResultCount = limit,
                SkipCount = offset,
                Sorting = sort
            };
        }
    }
}
