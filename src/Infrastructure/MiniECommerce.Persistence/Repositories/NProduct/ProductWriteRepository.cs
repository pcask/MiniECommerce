using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NProduct
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
