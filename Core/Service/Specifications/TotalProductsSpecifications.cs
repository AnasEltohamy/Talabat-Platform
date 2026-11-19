using DomainLayer.Models.ProductModels;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class TotalProductsSpecifications:BaseSpecifications<Product, int>
    {
        public TotalProductsSpecifications(ProductQueryParameters queryParameters) :base(BuildCriteria(queryParameters))
        {
            
        }
        private static Expression<Func<Product, bool>> BuildCriteria(ProductQueryParameters q)
        {
            int? typeId = q.TypeId;
            int? brandId = q.BrandId;
            string? search = q.SearchValue?.ToLower();

            return P =>
                (!typeId.HasValue || P.TypeId == typeId.Value) &&
                (!brandId.HasValue || P.BrandId == brandId.Value) &&
                (string.IsNullOrWhiteSpace(search) || P.Name.ToLower().Contains(search));
        }

    }
}
