using MiniECommerce.Application.Repositories.NProductImageFile;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NProductImageFile
{
    public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
