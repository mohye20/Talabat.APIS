using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.APIS.Controllers
{
	public class BasketController : ApiBaseController
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository, IMapper mapper)
		{
			_basketRepository = basketRepository;
			_mapper = mapper;
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
		public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdateBasket(CustomerBasketDTO Basket)
		 {
			var MappedBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(Basket);

			var CreateOrUpdateBasket = await _basketRepository.UpdatBasketAsync(MappedBasket);
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