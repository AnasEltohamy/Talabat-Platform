using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presintation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    
    public class OrdersController(IServiceManager _serviceManager) :ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return await _serviceManager.OrderService.CreateOrderAsync(orderDTO, email);
        }
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _serviceManager.OrderService.GetAllOrdersAsync(email!);
            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid id) 
        {
            return Ok(await _serviceManager.OrderService.GetOrderByIdAsync(id));
        }

        
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetDeliveryMethods()
        {
            return Ok( await _serviceManager.OrderService.GetDeliveryMethodsAsync());
        }



    }
}
