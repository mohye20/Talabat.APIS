using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;

namespace Talabat.Core.Specifications
{
	public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product>
	{
		// CTOR is Used For Get All Products
		public ProductWithBrandAndTypeSpecification() : base()
		{
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
		}


		// CTOR is Used For Get Product With Id
        public ProductWithBrandAndTypeSpecification(int Id) :base(P=>P.Id == Id)
        {
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
		}
    }
}