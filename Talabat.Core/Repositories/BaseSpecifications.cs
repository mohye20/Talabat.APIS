using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
	public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

		// Get All
		public BaseSpecifications()
		{
		}

		// Get By Id

		public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
		{
			Criteria = criteriaExpression;
		}
	}
}