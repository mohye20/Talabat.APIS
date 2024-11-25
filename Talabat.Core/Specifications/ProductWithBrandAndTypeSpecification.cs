using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
	public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product>
	{
		// CTOR is Used For Get All Products
		public ProductWithBrandAndTypeSpecification(string Sort) : base()
		{
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);

			if (!string.IsNullOrEmpty(Sort))
			{
				switch (Sort)
				{
					case "PriceAsc":
						AddOrderBy(P => P.Price);
						break;

					case "PriceDesc":
						AddOrderByDescending(P => P.Price);
						break;

					default:
						AddOrderBy(P => P.Name);
						break;
				}
			}
		}

		// CTOR is Used For Get Product With Id
		public ProductWithBrandAndTypeSpecification(int Id) : base(P => P.Id == Id)
		{
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
		}
	}
}