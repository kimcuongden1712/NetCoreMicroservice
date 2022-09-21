using Basket.API.Entities;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories.Interfaces
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _serializeService;
        public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService)
        {
            _redisCacheService = redisCacheService;
            _serializeService = serializeService;
        }
        async Task<Cart?> IBasketRepository.GetBasketByUserName(string username)
        {
            var basket = await _redisCacheService.GetStringAsync(username);
            return string.IsNullOrEmpty(basket) ? null : _serializeService.Derialize<Cart>(basket);
        }

        Task<Cart> IBasketRepository.UpdateBasket(Cart cart, DistributedCacheEntryOptions options)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBasketRepository.DeleteBasket(string username)
        {
            throw new NotImplementedException();
        }
    }
}