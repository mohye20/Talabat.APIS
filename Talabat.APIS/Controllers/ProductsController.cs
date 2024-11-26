using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIS.Controllers
{
	public class ProductsController : ApiBaseController
	{
		private readonly IGenaricRepository<Product> _ProductRepo;
		private readonly IMapper _mapper;
		private readonly IGenaricRepository<ProductType> _typeRepo;
		private readonly IGenaricRepository<ProductBrand> _brandRepo;

		public ProductsController(IGenaricRepository<Product> ProductRepo, IMapper mapper, IGenaricRepository<ProductType> typeRepo, IGenaricRepository<ProductBrand> brandRepo)
		{
			_ProductRepo = ProductRepo;
			_mapper = mapper;
			_typeRepo = typeRepo;
			_brandRepo = brandRepo;
		}

		// Get All Products
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery] ProductSpecificationsParams Params)
		{
			var Specification = new ProductWithBrandAndTypeSpecification(Params);
			var Products = await _ProductRepo.GetAllWithSpecificationAsync(Specification);
			var MappedProduct = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(Products);
			var CountSpecification = new ProductWithFilteraionForCountAsync(Params);
			var Count =await  _ProductRepo.GetCountWithSpecificationAsync(CountSpecification);

			//var ReturnedObject = new Pagination<ProductToReturnDTO>
			//{
			//	PageIndex = Params.PageIndex,
			//	PageSize = Params.PageSize,
			//	Data = MappedProduct
			//};

			return Ok(new Pagination<ProductToReturnDTO>(Params.PageIndex, Params.PageSize, MappedProduct , Count));
			//OkObjectResult Result = new OkObjectResult(Products);
			//return Result
		}

		// Get Product By Id

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDTO), 200)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
		{
			var Specification = new ProductWithBrandAndTypeSpecification(id);
			var Product = await _ProductRepo.GetByIdWithSpecificationAsync(Specification);
			if (Product is null)
			{
				return NotFound(new ApiResponse(404));
			}
			var MappedProduct = _mapper.Map<Product, ProductToReturnDTO>(Product);
			return Ok(MappedProduct);
		}

		// Get All Types

		[HttpGet("Types")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
		{
			var Types = await _typeRepo.GetAllAsync();

			return Ok(Types);
		}

		// Get All Brands

		[HttpGet("Brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var Brands = await _brandRepo.GetAllAsync();
			return Ok(Brands);
		}
	}
}