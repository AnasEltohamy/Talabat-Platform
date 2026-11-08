using DomainLayer.Models;
using Shared;
using Shared.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAllProductAsync(ProductQueryParameters queryParameters);

        public Task<ProductDTO> GetProductByIdAsync(int id);

        public Task<IEnumerable<TypeDTO>> GetAllTypesAsync();

        public Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();



    }
}
