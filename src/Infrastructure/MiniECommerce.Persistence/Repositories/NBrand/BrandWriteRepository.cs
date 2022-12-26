using MiniECommerce.Application.Repositories.NBrand;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NBrand
{
    public class BrandWriteRepository : WriteRepository<Brand>, IBrandWriteRepository
    {
        public BrandWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
