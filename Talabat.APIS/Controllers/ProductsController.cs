using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIS.Controllers
{
	public class ProductsController : ApiBaseController
	{
		private readonly IGenaricRepository<Product> _ProductRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenaricRepository<Product> ProductRepo, IMapper mapper)
		{
			_ProductRepo = ProductRepo;
			_mapper = mapper;
		}

		// Get All Products

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var Specification = new ProductWithBrandAndTypeSpecification();
			var Products = await _ProductRepo.GetAllWithSpecificationAsync(Specification);
			var MappedProduct = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(Products);
			return Ok(MappedProduct);
			//OkObjectResult Result = new OkObjectResult(Products);
			//return Result
		}

		// Get Product By Id

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var Specification = new ProductWithBrandAndTypeSpecification(id);
			var Product = await _ProductRepo.GetByIdWithSpecificationAsync(Specification);
			var MappedProduct = _mapper.Map<Product, ProductToReturnDTO>(Product);
			return Ok(MappedProduct);
		}
	}
}