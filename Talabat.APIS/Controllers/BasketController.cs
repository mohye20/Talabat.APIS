using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.APIS.Controllers
{
	public class BasketController : ApiBaseController
	{
		private readonly IBasketRepository _basketRepository;

		public BasketController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}

		// GET Or ReCreate

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
		{
			var Basket = await _basketRepository.GetBasketAsync(BasketId);

			//if(Basket is null)
			//{
			//	return new CustomerBasket(BasketId);
			//}
			//return Ok(Basket);

			return Basket is null ? new CustomerBasket(BasketId) : Ok(Basket);
		}

		// Update

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Basket)
		{
			var CreateOrUpdateBasket = await _basketRepository.UpdatBasketAsync(Basket);
			if (CreateOrUpdateBasket is null)
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(CreateOrUpdateBasket);
		}

		// Delete

		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
		{
			return await _basketRepository.DeleteBasketAsync(BasketId);

		}
	}
}