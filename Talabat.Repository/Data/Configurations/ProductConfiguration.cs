using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data.Configurations
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasOne(P => P.ProductBrand)
				.WithMany()
				.HasForeignKey(P => P.ProductBrandId);

			builder.HasOne(P => P.ProductType)
				.WithMany()
				.HasForeignKey(P => P.ProductTypeId);

			builder.Property(P => P.Name).HasMaxLength(100).IsRequired();
			builder.Property(P => P.Description).IsRequired();
			builder.Property(P => P.PictureUrl).IsRequired();
			builder.Property(P => P.Price).HasColumnType("decimal(18,2)");

		}
	}
}