using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Domain.Entities;

namespace MiniECommerce.Application.Features.Commands.NProduct.DeleteProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            product?.ProductImageFiles.Remove(product.ProductImageFiles.FirstOrDefault(i => i.Id == Guid.Parse(request.ImageId)));

            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
