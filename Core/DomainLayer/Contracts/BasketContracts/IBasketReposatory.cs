using DomainLayer.Models.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts.BasketContracts
{
    public interface IBasketReposatory
    {
        Task<CustomerBasket?> GetCustomerBasketAsync(string Key);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket ,TimeSpan? TimeToLive = null);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
