using MiniECommerce.Application.Repositories.NAppUserAddress;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NAppUserAddress
{
    public class AppUserAddressWriteRepository : WriteRepository<AppUserAddress>, IAppUserAddressWriteRepository
    {
        public AppUserAddressWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
