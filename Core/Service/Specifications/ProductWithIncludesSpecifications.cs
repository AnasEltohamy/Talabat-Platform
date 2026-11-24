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
    public class ProductWithIncludesSpecifications : BaseSpecifications<Product,int>
    {

        public  ProductWithIncludesSpecifications(ProductQueryParameters queryParameters) : base(BuildCriteria(queryParameters))
        {
            AddInclude(P=>P.ProductType);
            AddInclude(P=>P.ProductBrand);
            ApplyPagination(queryParameters.PageSize *(queryParameters.PageIndex-1), queryParameters.PageSize);

            switch(queryParameters.sort)
            {
                case ProductSortingOptions.name: AddOrderByAsc(P=>P.Name);
                    break;
                case ProductSortingOptions.priceAsc : AddOrderByAsc(P=>P.Price);
                    break;
                case ProductSortingOptions.priceDesc: AddOrderByDesc(P=>P.Price);
                    break;
                
            }
        }
        public ProductWithIncludesSpecifications(int id ):base(P=>P.Id ==id)
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }




        private static Expression<Func<Product, bool>> BuildCriteria(ProductQueryParameters q)
        {
            int? typeId = q.TypeId;
            int? brandId = q.BrandId;
            string? search = q.search?.ToLower();

            return P =>
                (!typeId.HasValue || P.TypeId == typeId.Value) &&
                (!brandId.HasValue || P.BrandId == brandId.Value) &&
                (string.IsNullOrWhiteSpace(search) || P.Name.ToLower().Contains(search));
        }

    }
}
