using Talabat.Core.Entites;

namespace Talabat.Core.Repositories
{
	public interface IGenaricRepository<T> where T : BaseEntity
	{
		// Get All
		Task<IEnumerable<T>> GetAllAsync();

		// Get By Id
		Task<T> GetByIdAsync(int id);
	}
}