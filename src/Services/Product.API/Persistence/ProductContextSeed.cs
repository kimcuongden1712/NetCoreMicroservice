using Product.API.Entities;
using Ilogger = Serilog.ILogger;

namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedAsync(ProductContext productContext, Ilogger logger)
        {
            if (!productContext.Products.Any())
            {
                productContext.AddRange(GetCatelogProducts());
                
                await productContext.SaveChangesAsync();

                logger.Information("Seed database associated with context {DbContextName}", typeof(ProductContext).Name);
            }
        }

        private static IEnumerable<CatelogProduct> GetCatelogProducts()
        {
            return new List<CatelogProduct>
                {
                    new CatelogProduct
                    {
                        No = "P0001",
                        Name = "Product 1",
                        Description = "Product 1 Description",
                        Price = 100,
                        PictureFileName = "product1.jpg",
                        PictureUri = "http://externalproduct1.jpg",
                        Category = "Category 1",
                        Brand = "Brand 1",
                        Summary = "Product 1 Summary",
                        AvailableStock = 100,
                        CreatedDate = DateTimeOffset.UtcNow
                    },
                    new CatelogProduct
                    {
                        No = "P0002",
                        Name = "Product 2",
                        Description = "Product 2 Description",
                        Price = 200,
                        PictureFileName = "product2.jpg",
                        PictureUri = "http://externalproduct2.jpg",
                        Category = "Category 2",
                        Brand = "Brand 2",
                        Summary = "Product 2 Summary",
                        AvailableStock = 200,
                        CreatedDate = DateTimeOffset.UtcNow
                    }
                };
        }
    }
}
