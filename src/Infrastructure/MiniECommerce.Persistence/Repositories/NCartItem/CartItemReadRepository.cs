using MiniECommerce.Application.Repositories.NCartItem;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Repositories.NCartItem
{
    public class CartItemReadRepository : ReadRepository<CartItem>, ICartItemReadRepository
    {
        public CartItemReadRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
