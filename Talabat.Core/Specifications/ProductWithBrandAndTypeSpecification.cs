using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
	public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product>
	{
		// CTOR is Used For Get All Products
		public ProductWithBrandAndTypeSpecification(ProductSpecificationsParams Params) : base
			(P =>
			(string.IsNullOrEmpty(Params.Search) || P.Name.ToLower().Contains(Params.Search))
			&&
			(!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId)
			&&
			(!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
			)
		{
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);

			if (!string.IsNullOrEmpty(Params.Sort))
			{
				switch (Params.Sort)
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

			ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);
		}

		// CTOR is Used For Get Product With Id
		public ProductWithBrandAndTypeSpecification(int Id) : base(P => P.Id == Id)
		{
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
		}
	}
}