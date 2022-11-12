using MiniECommerce.Application.Repositories.NInvoiceFile;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;

namespace MiniECommerce.Persistence.Repositories.NInvoiceFile
{
    public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
