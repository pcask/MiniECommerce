using MediatR;
using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Domain.Entities;

namespace MiniECommerce.Application.Features.Queries.NProduct.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            Product product = await _productReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Name = product.Name,
                AmountOfStock = product.AmountOfStock,
                Price = product.Price
            };
        }
    }
}
