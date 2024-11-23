using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenaricRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext) // Ask CLr For Creating Object From DbContext Implicitly
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

		public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

		//=> await _dbContext.Set<T>().Where(X => X.Id == id).FirstOrDefaultAsync();
	}
}