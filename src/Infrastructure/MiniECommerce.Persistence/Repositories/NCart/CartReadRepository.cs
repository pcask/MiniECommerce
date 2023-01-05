using MiniECommerce.Application.Repositories.NCart;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Repositories.NCart
{
    public class CartReadRepository : ReadRepository<Cart>, ICartReadRepository
    {
        public CartReadRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
