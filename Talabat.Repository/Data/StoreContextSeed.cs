using System.Data;
using System.Text.Json;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		// Seeding
		public static async Task SeedAsync(StoreContext dbContext)
		{
			if (!dbContext.ProductBrands.Any())
			{
				var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
				var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
				if (Brands?.Count > 0)
				{
					foreach (var Brand in Brands)
					{
						await dbContext.Set<ProductBrand>().AddAsync(Brand);
					}
				}
				await dbContext.SaveChangesAsync();
			}

			// Seeding Types
			if (!dbContext.ProductTypes.Any())
			{
				var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
				var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
				if (Types?.Count > 0)
				{
					foreach (var Type in Types)
					{
						await dbContext.Set<ProductType>().AddAsync(Type);
					}
					await dbContext.SaveChangesAsync();
				}
			}

			// Seeding Product

			if (!dbContext.Products.Any())
			{
				var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
				var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
				if (Products?.Count > 0)
				{
					foreach (var Product in Products)
					{
						await dbContext.Set<Product>().AddAsync(Product);
					}

					await dbContext.SaveChangesAsync();
				}
			}

			if (!dbContext.DeliveryMethods.Any())
			{
				var DeliveryMethodData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
				var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
				if(DeliveryMethods?.Count > 0)
				{
					foreach (var deliveryMethod in DeliveryMethods)
					{
						await dbContext.Set<DeliveryMethod>().AddAsync(deliveryMethod);
					}
					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}