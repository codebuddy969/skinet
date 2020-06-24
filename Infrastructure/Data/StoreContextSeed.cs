using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        // Static method allows us to use a method inside the class without a need of creating a new instance of the class before we can use it's method.
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            // Because we're running the seed method inside of a programm class, we're not gonna have any global exception handling available here
            try
            {
                // If there aren't any brands in the database we will seed the database from the brands.json file
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    // Save all changes to database
                    await context.SaveChangesAsync();
                }

                // If there aren't any types in the database we will seed the database from the types.json file
                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    // Save all changes to database
                    await context.SaveChangesAsync();
                }
                
                // If there aren't any products in the database we will seed the database from the products.json file
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    // Save all changes to database
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(e.Message);
            }
        }
    }
}