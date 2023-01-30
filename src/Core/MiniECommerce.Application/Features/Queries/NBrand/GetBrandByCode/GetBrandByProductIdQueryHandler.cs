using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Repositories.NProduct;

namespace MiniECommerce.Application.Features.Queries.NBrand.GetBrandByCode
{
    public class GetBrandByProductIdQueryHandler : IRequestHandler<GetBrandByProductIdQueryRequest, GetBrandByProductIdQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetBrandByProductIdQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetBrandByProductIdQueryResponse> Handle(GetBrandByProductIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table.Include(b => b.Brand).FirstOrDefaultAsync(p => p.Id.ToString() == request.ProductId);

            return new()
            {
                Code = product.BrandCode,
                Name = product.Brand.Name
            };
        }
    }
}
