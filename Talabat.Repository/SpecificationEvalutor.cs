﻿using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	public static class SpecificationEvalutor<T> where T : BaseEntity
	{
		// Fun To Build Query

		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
		{
			var Query = inputQuery; // _dbContext.Set<T>()

			if (specification.Criteria is not null)
			{
				Query = Query.Where(specification.Criteria); // _dbContext.Set<T>().Where(P=> P.Id == id)
			}

			// P=> P.ProductBrand , P=> P.ProductType
			Query = specification.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
			return Query;
		}
	}
}