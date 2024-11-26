using System.Linq.Expressions;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
	public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDescending { get; set; }
		public int Take { get; set; }
		public int Skip { get; set; }
		public bool IsPaginationEnbaled { get; set; }

		// Get All
		public BaseSpecifications()
		{
		}

		// Get By Id

		public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
		{
			Criteria = criteriaExpression;
		}

		public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}

		public void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
		{
			OrderByDescending = orderByDescendingExpression;
		}

		public void ApplyPagination(int skip, int take)
		{
			IsPaginationEnbaled = true;
			Skip = skip;
			Take = take;
		}
	}
}