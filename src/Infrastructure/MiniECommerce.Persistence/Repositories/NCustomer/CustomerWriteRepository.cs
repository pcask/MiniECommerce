using MiniECommerce.Application.Repositories.NCustomer;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NCustomer
{
    public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
