using DomainLayer.Models.BasketModels;
using Shared.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<CustomerBasketDTO> GetBasketAsync(string id);
        Task<CustomerBasketDTO> CreateOrUpdateBasketAsync(CustomerBasketDTO Basket);

        Task<bool> DeleteBasketAsync(string id);

    }
}
