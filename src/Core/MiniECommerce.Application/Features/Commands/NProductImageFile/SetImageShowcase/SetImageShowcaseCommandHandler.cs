using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Repositories.NProductImageFile;

namespace MiniECommerce.Application.Features.Commands.NProductImageFile.SetImageShowcase
{
    public class SetImageShowcaseCommandHandler : IRequestHandler<SetImageShowcaseCommandRequest, SetImageShowcaseCommandResponse>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public SetImageShowcaseCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<SetImageShowcaseCommandResponse> Handle(SetImageShowcaseCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table.Include(f => f.Products)
                  .Where(f => f.Products.FirstOrDefault(p => p.Id == Guid.Parse(request.ProductId)) != null);

            var images = await query.Where(f => f.Showcase == true).ToListAsync();

            images?.ForEach(f => f.Showcase = false);

            var image = await query.FirstOrDefaultAsync(f => f.Id == Guid.Parse(request.ImageId));

            image.Showcase = true;

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
