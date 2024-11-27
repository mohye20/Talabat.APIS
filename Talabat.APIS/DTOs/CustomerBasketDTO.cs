using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entites;

namespace Talabat.APIS.DTOs
{
	public class CustomerBasketDTO
	{

		[Required]
		public string Id { get; set; }

		public List<BasketItemDTO> Items { get; set; }

	}
}
