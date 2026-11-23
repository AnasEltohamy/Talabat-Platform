using Microsoft.AspNetCore.Authorization;
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
    public class PaymentsController(IServiceManager _serviceManager) : ControllerBase
    {
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDTO>> CreatePaymentIntent(string basketId)
        {
            return Ok(await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId));
        }
    }
}
