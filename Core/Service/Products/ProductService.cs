using AutoMapper;
using DomainLayer.Contracts.ProductContracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModels;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Products
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        

        public async Task<PaginatedResult<ProductDTO>> GetAllProductAsync(ProductQueryParameters queryParameters)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Spec = new ProductWithIncludesSpecifications(queryParameters);
            var Products = await Repo.GetAllAsync(Spec);
            var PageSize = Products.Count();

            var totalCountSpecification = new TotalProductsSpecifications(queryParameters);
            var totalCount = await Repo.CountAsync(totalCountSpecification);
           
            var Mappeng = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products);
           
            return new PaginatedResult<ProductDTO>
                (
                    queryParameters.PageIndex,
                    PageSize,
                    totalCount,
                    Mappeng
                );
        }



        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var Spec = new ProductWithIncludesSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Spec);
            if (Product is null) 
            {
                throw new ProductNotFoundExcption(id);
            }
            return _mapper.Map<Product, ProductDTO>(Product);

        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var Mappeng = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTO>>(Brand);
            return Mappeng;

        }
        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            var Mappeng = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTO>>(Types);
            return Mappeng;
        }

       
    }
}
