using Blog.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Articles.Dto
{
    public class QueryAriticelInputDto : PagedResultRequestDto
    {
        public bool OnlyPublish { get; set; } = true;
    }
}
