using MediatR;
using MiniECommerce.Application.Abstractions.GoogleIdToken;
using MiniECommerce.Application.Abstractions.Services;

namespace MiniECommerce.Application.Features.Commands.NAppUser.LoginWithGoogle
{
    public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommandRequest, LoginWithGoogleCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginWithGoogleCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginWithGoogleCommandResponse> Handle(LoginWithGoogleCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginWithGoogleAsync(request.IdToken, 30);
            return new() { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken };
        }
    }
}
