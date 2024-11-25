using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
	public interface IGenaricRepository<T> where T : BaseEntity
	{
		#region Without Specification

		// Get All
		Task<IEnumerable<T>> GetAllAsync();

		// Get By Id
		Task<T> GetByIdAsync(int id);

		#endregion Without Specification

		#region With Specification

		Task<IEnumerable<T>>  GetAllWithSpecificationAsync(ISpecification<T> specification);

		Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification);



		#endregion With Specification
	}
}