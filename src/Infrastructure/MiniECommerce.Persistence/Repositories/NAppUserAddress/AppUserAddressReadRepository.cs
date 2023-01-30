using MiniECommerce.Application.Repositories.NAppUserAddress;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Repositories.NAppUserAddress
{
    public class AppUserAddressReadRepository : ReadRepository<AppUserAddress>, IAppUserAddressReadRepository
    {
        public AppUserAddressReadRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
