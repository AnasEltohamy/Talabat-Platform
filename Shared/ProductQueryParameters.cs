using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParameters
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? SearchValue { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
    }
}
