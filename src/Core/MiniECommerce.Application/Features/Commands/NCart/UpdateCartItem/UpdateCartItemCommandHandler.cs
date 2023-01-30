using MediatR;
using MiniECommerce.Application.Abstractions.Services;

namespace MiniECommerce.Application.Features.Commands.NCart.UpdateCartItem
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommandRequest, UpdateCartItemCommandResponse>
    {
        private readonly ICartService _cartService;

        public UpdateCartItemCommandHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<UpdateCartItemCommandResponse> Handle(UpdateCartItemCommandRequest request, CancellationToken cancellationToken)
        {
            await _cartService.UpdateCartItemAsync(new()
            {
                CartItemId = request.CartItemId,
                Quantity = request.Quantity,
                IsActive = request.IsActive
            });

            return new();
        }
    }
}
