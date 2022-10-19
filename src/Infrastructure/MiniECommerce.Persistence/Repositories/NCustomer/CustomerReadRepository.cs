using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Repositories.NCustomer;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Repositories.NCustomer
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
