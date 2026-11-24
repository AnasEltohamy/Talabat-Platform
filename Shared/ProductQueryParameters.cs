using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParameters
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;

      

        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? search { get; set; }
        public ProductSortingOptions sort { get; set; }

        private int _pageIndex = 1;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }


        private int _pageSize = DefaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < 1 ? DefaultPageSize : (value > MaxPageSize ? MaxPageSize : value);
        }
    }
}
