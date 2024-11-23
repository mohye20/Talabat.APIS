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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var Products = await _ProductRepo.GetAllAsync();
			return Ok(Products);
			//OkObjectResult Result = new OkObjectResult(Products);
			//return Result
		}

		// Get Product By Id

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var Product = await _ProductRepo.GetByIdAsync(id);
			return Ok(Product);
		}
	}
}