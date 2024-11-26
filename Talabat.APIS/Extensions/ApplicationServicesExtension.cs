using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIS.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
		{
			Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

			Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenericRepository<>));
			Services.AddAutoMapper(typeof(MappingProfiles));

			Services.Configure<ApiBehaviorOptions>(
				Options => Options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					// ModelState => Dic [KeyValuePair]
					// Key = > Name Of Param
					// Value = Errors
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
														 .SelectMany(P => P.Value.Errors)
														 .Select(E => E.ErrorMessage)
														 .ToArray();
					var ValidationErrorResponce = new ApiValidationErrorResponse()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(ValidationErrorResponce);
				}
				);

			return Services;
		}
	}
}