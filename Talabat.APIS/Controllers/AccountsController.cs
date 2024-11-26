using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entites.Identity;

namespace Talabat.APIS.Controllers
{
	public class AccountsController : ApiBaseController
	{
		private readonly UserManager<AppUser> _userManager;

		public AccountsController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		// Register

		[HttpPost("Register")]
		public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
		{
			var User = new AppUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.Email.Split('@')[0],
				PhoneNumber = model.PhoneNumber
			};

			var Result = await _userManager.CreateAsync(User, model.Password);

			if (!Result.Succeeded)
			{
				return BadRequest(new ApiResponse(400));
			}

			var ReturnedUser = new UserDTO()
			{
				DisplayName = User.DisplayName,
				Email = User.Email,
				Token = "ThisWillBeToken"
			};

			return ReturnedUser; 
		}
	}
}