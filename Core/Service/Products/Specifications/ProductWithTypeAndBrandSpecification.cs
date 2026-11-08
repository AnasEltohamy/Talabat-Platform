using DomainLayer.Models.ProductModels;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Products.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product,int>
    {

        public ProductWithTypeAndBrandSpecification(ProductQueryParameters queryParameters) : base(BuildCriteria(queryParameters))
        {
            AddInclude(P=>P.ProductType);
            AddInclude(P=>P.ProductBrand);


            switch(queryParameters.SortingOptions)
            {
                case ProductSortingOptions.SortByNameAsc: AddOrderByAsc(P=>P.Name);
                    break;
                case ProductSortingOptions.SortByPriceAsc : AddOrderByAsc(P=>P.Price);
                    break;
                case ProductSortingOptions.SortByNameDesc: AddOrderByDesc(P=>P.Name);
                    break;
                case ProductSortingOptions.SortByPriceDesc: AddOrderByDesc(p=>p.Price);
                    break;
            }
        }
        public ProductWithTypeAndBrandSpecification(int id ):base(P=>P.Id ==id)
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
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
