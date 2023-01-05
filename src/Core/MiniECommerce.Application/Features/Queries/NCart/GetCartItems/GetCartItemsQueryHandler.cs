using MediatR;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.DTOs.NCartItem;

namespace MiniECommerce.Application.Features.Queries.NCart.GetCartItems
{
    public class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQueryRequest, List<GetCartItemsQueryResponse>>
    {
        private readonly ICartService _cartService;

        public GetCartItemsQueryHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<List<GetCartItemsQueryResponse>> Handle(GetCartItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var cartItems = await _cartService.GetCartItemsAsync();

            return cartItems.Select(ci => new GetCartItemsQueryResponse
            {
                CartItemId = ci.Id.ToString(),
                Name = ci.Product.Name,
                Price = ci.Product.Price.ToString(),
                Quantity = ci.Quantity
            }).ToList();
        }
    }
}
