using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Repositories
{
	public interface IBasketRepository
	{
		 Task<CustomerBasket?> GetBasketAsync(string BasketId);

		 Task<CustomerBasket?> UpdatBasketAsync(CustomerBasket Basket);

		 Task<bool> DeleteBasketAsync(string BasketId);
	}
}