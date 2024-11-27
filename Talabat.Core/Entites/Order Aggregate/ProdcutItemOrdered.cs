using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Order_Aggregate
{
	public class ProdcutItemOrdered
	{
		public ProdcutItemOrdered()
		{
		}

		public ProdcutItemOrdered(int prodcutId, string productName, string productUrl)
		{
			ProdcutId = prodcutId;
			ProductName = productName;
			ProductUrl = productUrl;
		}

		public int ProdcutId { get; set; }

		public string ProductName { get; set; }

		public string ProductUrl { get; set; }
	}
}