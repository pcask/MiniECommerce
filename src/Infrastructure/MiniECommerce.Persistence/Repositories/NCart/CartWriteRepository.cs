using MiniECommerce.Application.Repositories.NCart;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NCart
{
    public class CartWriteRepository : WriteRepository<Cart>, ICartWriteRepository
    {
        public CartWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
