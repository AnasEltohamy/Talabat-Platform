using DomainLayer.Contracts.CashingContracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Cashing
{
    public class CacheService:ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public Task<string?> GetAsync(string cacheKey)
            => _cacheRepository.GetAsync(cacheKey);

        public Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive)
            => _cacheRepository.SetAsync(cacheKey, cacheValue, timeToLive);
    }
}
