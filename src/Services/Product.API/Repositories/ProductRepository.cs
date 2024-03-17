using Contracts.Commonn.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository : RepositoryBaseAsync<CatelogProduct, long, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<IEnumerable<CatelogProduct>> GetProductsAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<CatelogProduct> GetProductAsync(long id)
        {
            return await FindByCondition(p => p.Id.Equals(id), trackChanges: false).SingleOrDefaultAsync();
        }

        public async Task<CatelogProduct> GetProductByNo(string productNo)
        {
            return await FindByCondition(p => p.No.Equals(productNo), trackChanges: false).SingleOrDefaultAsync();
        }

        public async Task CreateProduct(CatelogProduct product) => await CreateAsync(product);

        public async Task UpdateProduct(CatelogProduct product) => await UpdateAsync(product);

        public async Task DeleteProduct(long id)
        {
            var product = await GetProductAsync(id);
            await DeleteAsync(product);
        }
    }
}
