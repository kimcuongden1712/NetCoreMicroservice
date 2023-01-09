using Contracts.Common.Interfaces;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces
{
    public interface IAppUnitOfWork : IUnitOfWork<ProductContext>
    {
    }
}
