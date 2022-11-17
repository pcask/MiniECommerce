using MediatR;
using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Domain.Entities;

namespace MiniECommerce.Application.Features.Commands.NProduct.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = await _productReadRepository.GetByIdAsync(request.Id);
            product.Name = request.Name;
            product.AmountOfStock = request.AmountOfStock;
            product.Price = request.Price;

            await _productWriteRepository.SaveAsync();
            return new()
            {
                Id = request.Id,
                Name = request.Name,
                AmountOfStock = request.AmountOfStock,
                Price = request.Price,
            };
        }
    }
}
