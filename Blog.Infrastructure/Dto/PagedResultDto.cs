using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dto
{
    public class PagedResultDto<T>
    {
        private IReadOnlyList<T> _items;
      
		public IReadOnlyList<T> Items
        {
            get
            {
                IReadOnlyList<T> tmp;
                if ((tmp = this._items) == null)
                {
                    tmp = (this._items = new List<T>());
                }
                return tmp;
            }
            set
            {
                this._items = value;
            }
        }

		public int TotalCount
        {
            get;
            set;
        }
       
        public PagedResultDto(int totalCount, IReadOnlyList<T> items)
        {
            this.Items = items;
            this.TotalCount = totalCount;
        }
    }
}
