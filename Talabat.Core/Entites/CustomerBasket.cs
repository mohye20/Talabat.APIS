using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
	internal class CustomerBasket
	{
		public decimal Id { get; set; }

		public List<BasketItem> Items { get; set; }
	}
}