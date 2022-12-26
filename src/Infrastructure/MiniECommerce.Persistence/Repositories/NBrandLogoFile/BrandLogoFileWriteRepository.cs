using MiniECommerce.Application.Repositories.NBrandLogoFile;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NBrandLogoFile
{
    public class BrandLogoFileWriteRepository : WriteRepository<BrandLogoFile>, IBrandLogoFileWriteRepository
    {
        public BrandLogoFileWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
