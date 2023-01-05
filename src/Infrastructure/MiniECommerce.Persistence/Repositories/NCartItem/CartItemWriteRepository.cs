using MiniECommerce.Application.Repositories.NCartItem;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NCartItem
{
    public class CartItemWriteRepository : WriteRepository<CartItem>, ICartItemWriteRepository
    {
        public CartItemWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
