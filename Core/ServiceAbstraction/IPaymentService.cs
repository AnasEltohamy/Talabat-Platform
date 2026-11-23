using Shared.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<CustomerBasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId);
    }
}
