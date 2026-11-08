using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presintation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager): ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdateBasketAsync(CustomerBasketDTO BasketDTO)
        {
           return await _serviceManager.basketService.CreateOrUpdateBasketAsync(BasketDTO);
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasketDTO>> GetCustomerBasketAsync(string id)
        {
            return Ok(await _serviceManager.basketService.GetBasketAsync(id));
        }

        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string Key)
        {
            return Ok(await _serviceManager.basketService.DeleteBasketAsync(Key));
        }
    }
}
