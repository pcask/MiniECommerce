using MediatR;
using MiniECommerce.Application.Abstractions.Hubs;
using MiniECommerce.Application.Repositories.NProduct;

namespace MiniECommerce.Application.Features.Commands.NProduct.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
        {
            _productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                AmountOfStock = request.AmountOfStock,
                Price = request.Price,
                BrandCode = request.BrandCode
            });

            await _productWriteRepository.SaveAsync();

            await _productHubService.ProductAddedMessageAsync($"Product with {request.Name} name has been added.");

            return new();
        }
    }
}
