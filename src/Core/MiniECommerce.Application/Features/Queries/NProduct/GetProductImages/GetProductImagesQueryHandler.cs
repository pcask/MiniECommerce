using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Domain.Entities;

namespace MiniECommerce.Application.Features.Queries.NProduct.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IConfiguration _configuration;

        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            return product.ProductImageFiles.Select(i => new GetProductImagesQueryResponse
            {
                Id = i.Id,
                FileName = i.FileName,
                Path = $"{_configuration["StorageBaseURL"]}/{i.FileName}",
                CreatedDate = i.CreatedDate
            }).ToList();
        }
    }
}
