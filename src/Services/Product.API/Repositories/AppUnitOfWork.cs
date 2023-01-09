using Contracts.Common.Interfaces;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class AppUnitOfWork : UnitOfWork<ProductContext>, IAppUnitOfWork
    {
        private ProductRepository _productRepository;
        public AppUnitOfWork(ProductContext context) : base(context)
        {
        }

    }
}
