using Contracts.Commonn.Interfaces;
using Product.API.Entities;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBaseAsync<CatelogProduct, long, ProductContext>
    {
        Task<IEnumerable<CatelogProduct>> GetProductsAsync();
        Task<CatelogProduct> GetProductAsync(long id);
        Task<CatelogProduct> GetProductByNo(string productNo);
        Task CreateProduct(CatelogProduct product);
        Task UpdateProduct(CatelogProduct product);
        Task DeleteProduct(long id);
    }
}
