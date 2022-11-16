using MediatR;
using MiniECommerce.Application.Repositories.NProduct;

namespace MiniECommerce.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                AmountOfStock = request.AmountOfStock,
                Price = request.Price
            });

            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
