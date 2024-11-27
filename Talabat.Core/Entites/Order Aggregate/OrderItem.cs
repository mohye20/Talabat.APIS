using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Order_Aggregate
{
	public class OrderItem : BaseEntity
	{

		public OrderItem()
		{
			
		}
		public OrderItem(ProdcutItemOrdered product, int quintity, decimal price)
		{
			Product = product;
			Quintity = quintity;
			Price = price;
		}

		public ProdcutItemOrdered Product { get; set; }


		public int Quintity { get; set; }

		public decimal Price { get; set; }
	}
}