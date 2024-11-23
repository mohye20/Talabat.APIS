using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.Core.Entites;
using Talabat.Repository.Data.Configurations;

namespace Talabat.Repository.Data
{
	public class StoreContext : DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Product> Products { get; set; }

		public DbSet<ProductType> ProductTypes { get; set; }

		public DbSet<ProductBrand> ProductBrands { get; set; }
	}
}