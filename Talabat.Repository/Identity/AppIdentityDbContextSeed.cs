using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entites.Identity;

namespace Talabat.Repository.Identity
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var User = new AppUser()
				{
					DisplayName = "Mohye",
					Email = "Mohye20@gmail.com",
					UserName = "Mohye20",
					PhoneNumber = "01151285511"
				};
				await userManager.CreateAsync(User, "P@ssW0rd");
			}
		}
	}
}