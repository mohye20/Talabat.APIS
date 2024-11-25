using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;

namespace Talabat.APIS.Controllers
{
	[Route("error/{Code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		public ActionResult Error(int Code)
		{
			return NotFound(new ApiResponse(Code));
		}
	}
}