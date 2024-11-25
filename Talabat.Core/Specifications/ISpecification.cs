using System.Linq.Expressions;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
	public interface ISpecification<T> where T : BaseEntity
	{
		//Signature For Porperty For Where Condition [Where(P=>P.id == id)]

		public Expression<Func<T, bool>> Criteria { get; set; }

		// Signature For Property For List Of Include [Include(P=>P.ProductBrand).Include(P=> P.ProductType)]

		public List<Expression<Func<T, object>>> Includes { get; set; }
	}
}