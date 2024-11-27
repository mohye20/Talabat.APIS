using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services;

namespace Talabat.APIS.Controllers
{
	public class AccountsController : ApiBaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

		public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
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
				Token = await _tokenService.CreateTokenAsync(User, _userManager)
			};

			return ReturnedUser;
		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
		{
			var User = await _userManager.FindByEmailAsync(model.Email);

			if (User is null)
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
				Token = await _tokenService.CreateTokenAsync(User, _userManager)
			});
		}

		[Authorize]
		[HttpGet("GetCurentUser")]

		public async Task<ActionResult<UserDTO>> GetCurentUser()
		{
			var Email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(Email);
			var ReturendObject = new UserDTO()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _tokenService.CreateTokenAsync(user, _userManager),

			};
			return Ok(ReturendObject);

		}

	}
}