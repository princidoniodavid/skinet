using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!storeContext.ProductBrands.Any())
            {
                var brandsData = File.OpenRead("../Infrastructure/Data/SeedData/brands.json");
                var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(brandsData);

                foreach (var brand in brands!)
                {
                    await storeContext.ProductBrands.AddAsync(brand);
                }

                await storeContext.SaveChangesAsync();
            }

            if (!storeContext.ProductsTypes.Any())
            {
                var typesData = File.OpenRead("../Infrastructure/Data/SeedData/types.json");
                var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(typesData);

                foreach (var type in types!)
                {
                    await storeContext.ProductsTypes.AddAsync(type);
                }

                await storeContext.SaveChangesAsync();
            }

            if (!storeContext.Products.Any())
            {
                var productData = File.OpenRead("../Infrastructure/Data/SeedData/products.json");
                var products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);

                foreach (var product in products!)
                {
                    await storeContext.Products.AddAsync(product);
                }

                await storeContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}