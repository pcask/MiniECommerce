using MediatR;
using MiniECommerce.Application.Abstractions.Services;

namespace MiniECommerce.Application.Features.Commands.NAppUser.LoginWithFacebook
{
    public class LoginWithFacebookCommandHandler : IRequestHandler<LoginWithFacebookCommandRequest, LoginWithFacebookCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginWithFacebookCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginWithFacebookCommandResponse> Handle(LoginWithFacebookCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginWithFacebookAsync(request.AuthToken, 60 * 60);
            return new() { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken };
        }
    }
}
