using Contracts.Common.Interfaces;
using Product.API.Persistence;

namespace Product.API.UnitOfWork
{
    public class UOFProduct : UnitOfWork<ProductContext>
    {
        private ProductContext _context;

        public UOFProduct(ProductContext context) : base(context)
        {
            this._context = context;
        }
    }
}