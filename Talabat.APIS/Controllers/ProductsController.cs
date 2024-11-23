using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.APIS.Controllers
{
	public class ProductsController : ApiBaseController
	{
		private readonly IGenaricRepository<Product> _ProductRepo;

		public ProductsController(IGenaricRepository<Product> ProductRepo)
		{
			_ProductRepo = ProductRepo;
		}

		// Get All Products

		// Get Product By Id
	}
}