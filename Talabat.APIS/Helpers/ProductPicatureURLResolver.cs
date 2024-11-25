using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIS.DTOs;
using Talabat.Core.Entites;

namespace Talabat.APIS.Helpers
{
	public class ProductPicatureURLResolver : IValueResolver<Product, ProductToReturnDTO, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPicatureURLResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{_configuration["ApiBaseURL"]}{source.PictureUrl}";
			}

			return string.Empty;
		}
	}
}