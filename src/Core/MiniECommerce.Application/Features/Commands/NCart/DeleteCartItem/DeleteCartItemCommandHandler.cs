using MediatR;
using MiniECommerce.Application.Abstractions.Services;

namespace MiniECommerce.Application.Features.Commands.NCart.DeleteCartItem
{
    public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommandRequest, DeleteCartItemCommandResponse>
    {
        private readonly ICartService _cartService;

        public DeleteCartItemCommandHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<DeleteCartItemCommandResponse> Handle(DeleteCartItemCommandRequest request, CancellationToken cancellationToken)
        {
            await _cartService.DeleteCartItemAsync(request.CartItemId);

            return new();
        }
    }
}
