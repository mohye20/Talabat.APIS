using Microsoft.AspNetCore.Identity;
using System.ComponentModel.Design;
using Talabat.Core.Entites.Identity;
using Talabat.Repository.Identity;

namespace Talabat.APIS.Extensions
{
	public static class IdentityServicesExtenstions
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection Services)
		{
			Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>();

			Services.AddAuthentication();


			return Services;
		}
	}
}