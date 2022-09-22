using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        public Task<Cart?> GetBasketByUserName(string username);

        public Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null);

        public Task<bool> DeleteBasket(string username);
    }
}