using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.Design;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.APIS.Extensions
{
	public static class IdentityServicesExtenstions
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection Services)
		{
			Services.AddScoped<ITokenService , TokenService>();

			Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>();

			Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer();


			return Services;
		}
	}
}