using DomainLayer.Contracts.BasketContracts;
using DomainLayer.Models.BasketModels;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Peresistence.Store.Data.Repositories
{
    public class BasketReposatory(IConnectionMultiplexer connectionMultiplexer) : IBasketReposatory
    {

        private readonly StackExchange.Redis.IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonObject = JsonSerializer.Serialize(basket);
            var Flag= _database.StringSetAsync(basket.Id, JsonObject, TimeToLive?? TimeSpan.FromDays(30));
            if (Flag is null)
            {
                return null;
            }
            else
            {
                return await GetCustomerBasketAsync(basket.Id);   
            }
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
             return await _database.KeyDeleteAsync(Key);
        }

        public async Task<CustomerBasket?> GetCustomerBasketAsync(string Key)
        {
            var Basket = await _database.StringGetAsync(Key);
            if (string.IsNullOrEmpty(Basket))
            {
                return null;
            }
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
            }
        }
    }
}
