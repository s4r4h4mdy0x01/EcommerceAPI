using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        #region Fields
        public IDistributedCache _redisCache;
        #endregion
        #region Constructors
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        #endregion

        #region Methods


        public async Task<ShoppingCart> GetBasketByUserNameAsync(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket)!;
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart cart)
        {
            var basket = await _redisCache.GetStringAsync(cart.UserName);
            if (basket != null)
            {
                return await GetBasketByUserNameAsync(cart.UserName);
            }
            else
            {
                await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
                return await GetBasketByUserNameAsync(cart.UserName);
            }
        }
        public async Task DeleteBasketAsync(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (basket != null)
            {
                await _redisCache.RemoveAsync(userName);
            }
        }
        #endregion

    }
}
