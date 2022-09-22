using Basket.API.Entities;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Ilogger = Serilog.ILogger;

namespace Basket.API.Repositories.Interfaces
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _serializeService;
        private readonly Ilogger _logger;
        public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, Ilogger logger)
        {
            _redisCacheService = redisCacheService;
            _serializeService = serializeService;
            _logger = logger;
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

        public async Task<Cart?> GetBasketByUserName(string username)
        {
            _logger.Information($"Begin GetBasketByUserName");
            var basket = await _redisCacheService.GetStringAsync(username);
            _logger.Information($"End GetBasketByUserName");
            return string.IsNullOrEmpty(basket) ? null : _serializeService.Derialize<Cart>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {
            _logger.Information($"Begin UpdateBasket");
            if (options != null)
            {
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart), options);
            }
            else
            {
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart));
            }
            _logger.Information($"Return UpdateBasket");
            return await GetBasketByUserName(cart.UserName);
        }
    }
}