using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
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

		// Old Version  With our Specification
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		// New version Specification Design Pattern
		public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).ToListAsync();
		}

		public async Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).FirstOrDefaultAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecification<T> specification)
		{
			return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), specification);
		}

		public async Task<int> GetCountWithSpecificationAsync(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).CountAsync();
		}

		public async Task Add(T item)

		=> await _dbContext.Set<T>().AddAsync(item);
	}
}