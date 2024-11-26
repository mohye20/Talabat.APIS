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
		private readonly SignInManager<AppUser> _signInManager;

		public AccountsController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager) 
		{
			_userManager = userManager;
			_signInManager = signInManager;
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

		[HttpPost("Login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
		{
			var User = await _userManager.FindByEmailAsync(model.Email);

			if(User is null)
			{
				return Unauthorized(new ApiResponse(401));
			}


			var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

			if (!Result.Succeeded)
			{

				return Unauthorized(new ApiResponse(401));
			}

			return Ok(new UserDTO()
			{
				DisplayName = User.DisplayName,
				Email = User.Email,
				Token = "This Will Be Token"

			});


		}
	}
}