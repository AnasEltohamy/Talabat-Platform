using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presintation.Attributes;
using ServiceAbstraction;
using Shared;
using Shared.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presintation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Roles ="Admin")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [Cache(60)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaginatedResult<ProductDTO>>>> GetAllProduct([FromQuery] ProductQueryParameters queryParameters )
        {
            return Ok(await _serviceManager.productService.GetAllProductAsync(queryParameters));
        }
        [Cache(60)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            return Ok(await _serviceManager.productService.GetProductByIdAsync(id));
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrand()
        {

            return Ok(await _serviceManager.productService.GetAllBrandsAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllType()
        {

            return Ok(await _serviceManager.productService.GetAllTypesAsync());
        }







    }
}
