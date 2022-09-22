using Basket.API.Entities;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Ilogger = Serilog.ILogger;

namespace Basket.API.Repositories.Interfaces
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _serializerService;
        private readonly Ilogger _logger;

        public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, Ilogger logger)
        {
            _redisCacheService = redisCacheService;
            _serializerService = serializeService;
            _logger = logger;
        }

        public async Task<Cart?> GetBasketByUserName(string username)
        {
            var basket = await _redisCacheService.GetStringAsync(username);
            return string.IsNullOrEmpty(basket) ? null :
                _serializerService.Deserialize<Cart>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart basket, DistributedCacheEntryOptions options = null)
        {
            if (options != null)
                await _redisCacheService.SetStringAsync(basket.UserName,
                    _serializerService.Serialize(basket), options);
            else
                await _redisCacheService.SetStringAsync(basket.UserName,
                _serializerService.Serialize(basket));

            return await GetBasketByUserName(basket.UserName);
        }

        public async Task<bool> DeleteBasket(string username)
        {
            try
            {
                await _redisCacheService.RemoveAsync(username);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("DeleteBasket" + ex.Message);
                return false;
            }
        }
    }
}