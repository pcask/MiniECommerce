using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.Repositories.NProduct;

namespace MiniECommerce.Application.Features.Queries.NCart.GetCartItems
{
    public class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQueryRequest, List<GetCartItemsQueryResponse>>
    {
        private readonly ICartService _cartService;
        private readonly IProductReadRepository _productReadRepository;

        public GetCartItemsQueryHandler(ICartService cartService, IProductReadRepository productReadRepository)
        {
            _cartService = cartService;
            _productReadRepository = productReadRepository;
        }

        public async Task<List<GetCartItemsQueryResponse>> Handle(GetCartItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartService.GetCartItemsAsync();

            return cartItems.OrderByDescending(ci => ci.CreatedDate).Select(ci => new GetCartItemsQueryResponse
            {
                CartItemId = ci.Id.ToString(),
                ProductId = ci.ProductId.ToString(),
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                Quantity = ci.Quantity,
                IsActive = ci.IsActive,
                BrandName = _productReadRepository.Table.Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == ci.ProductId).Result.Brand.Name,
                CreatedDate = ci.CreatedDate,
                ImagePath = ci.Product.ProductImageFiles.FirstOrDefault(i => i.Showcase == true)?.Path + "/" +
                            ci.Product.ProductImageFiles.FirstOrDefault(i => i.Showcase == true)?.FileName
            }).ToList();
        }
    }
}
