using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;

		public BasketRepository(IConnectionMultiplexer redis) // ASK CLR For Object From Class Interface IConnectionMultiplexer
		{
			_database = redis.GetDatabase();
		}

		public async Task<bool> DeleteBasketAsync(string BasketId)
		{
			return await _database.KeyDeleteAsync(BasketId);
		}

		public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
		{
			var Basket = await _database.StringGetAsync(BasketId);
			return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
		}

		public async Task<CustomerBasket?> UpdatBasketAsync(CustomerBasket Basket)
		{
			var JsonBasket = JsonSerializer.Serialize(Basket);
			var CreatedOrUpdate = await _database.StringSetAsync(Basket.Id, JsonBasket, TimeSpan.FromDays(1));
			if (!CreatedOrUpdate)
			{
				return null;
			}

			return await GetBasketAsync(Basket.Id);
		}
	}
}