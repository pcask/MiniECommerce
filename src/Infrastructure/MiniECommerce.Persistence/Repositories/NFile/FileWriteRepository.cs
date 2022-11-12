using MiniECommerce.Application.Repositories.NFile;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NFile
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
