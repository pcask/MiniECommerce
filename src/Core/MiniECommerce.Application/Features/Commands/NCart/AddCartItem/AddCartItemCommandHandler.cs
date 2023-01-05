using MediatR;
using MiniECommerce.Application.Abstractions.Services;

namespace MiniECommerce.Application.Features.Commands.NCart.AddCartItem
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommandRequest, AddCartItemCommandResponse>
    {
        private readonly ICartService _cartService;

        public AddCartItemCommandHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<AddCartItemCommandResponse> Handle(AddCartItemCommandRequest request, CancellationToken cancellationToken)
        {
            await _cartService.AddCartItemAsync(new()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            });

            return new();
        }
    }
}
