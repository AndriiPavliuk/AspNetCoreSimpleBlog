using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Dto
{
    public class PagedResultRequestDto
    {
        [Range(1, 2147483647)]
        public virtual int MaxResultCount { get; set; } = 10;
        [Range(0, 2147483647)]
        public virtual int SkipCount { get; set; } = 0;

        /// <summary>
        /// Sorting information.
        /// Should include sorting field and optionally a direction (ASC or DESC)
        /// Can contain more than one field separated by comma (,).
        /// </summary>
        /// <example>
        /// Examples:
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Age DESC"
        /// </summary>
        public virtual string Sorting
        {
            get;
            set;
        }
        public virtual int CurrentPage
        {
            get { return (SkipCount + MaxResultCount) / MaxResultCount; }
        }

        public void FetchFromOther(PagedResultRequestDto input)
        {
            this.SkipCount = input.SkipCount;
            this.Sorting = input.Sorting;
            this.MaxResultCount = input.MaxResultCount;
        }

    }
}
