using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIS.Controllers
{
	public class BuggyController : ApiBaseController
	{
		private readonly StoreContext _dbContext;

		public BuggyController(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}

		//BaseURl/api/buggy/NotFound

		[HttpGet("NotFound")]
		public ActionResult GetNotFoundRequest()
		{
			var Product = _dbContext.Products.Find(100);

			if (Product is null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok(Product);
		}

		[HttpGet("ServerError")]
		public ActionResult GetNotFoundResponse()
		{
			var Product = _dbContext.Products.Find(100);
			var ProdcutToReturn = Product.ToString(); // Error
													  // Will Thorw Exception Null Reference Exception
			return Ok(ProdcutToReturn);
		}

		[HttpGet("BadRequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("BadRequest/{id}")]
		public ActionResult GetBadRquest(int id)
		{
			return Ok();
		}
	}
}