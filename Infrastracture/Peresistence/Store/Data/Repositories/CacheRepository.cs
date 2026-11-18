using DomainLayer.Contracts.CashingContracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Peresistence.Store.Data.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;

        // camelCase = عشان الشكل يطابق رد الـ API العادي
        private static readonly JsonSerializerOptions _options = new(JsonSerializerDefaults.Web);

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string cacheKey)
        {
            var value = await _database.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive)
        {
            var json = JsonSerializer.Serialize(cacheValue, _options);
            await _database.StringSetAsync(cacheKey, json, timeToLive);
        }
    }
}
