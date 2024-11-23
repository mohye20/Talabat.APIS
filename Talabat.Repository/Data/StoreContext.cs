using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data
{
	public class StoreContext : DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

		public DbSet<ProductType> ProductTypes { get; set; }

		public DbSet<ProductBrand> ProductBrands { get; set; }
	}
}