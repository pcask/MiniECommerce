using MiniECommerce.Application.Repositories.NOrder;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NOrder
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
