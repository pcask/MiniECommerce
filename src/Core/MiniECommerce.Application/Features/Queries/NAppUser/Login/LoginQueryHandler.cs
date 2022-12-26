using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.Abstractions.Token;
using MiniECommerce.Application.Exceptions;
using MiniECommerce.Domain.Entities.Identity;

namespace MiniECommerce.Application.Features.Queries.NAppUser.LoginUser
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        private readonly IAuthService _authService;

        public LoginQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password, 60 * 60);
            return new() { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken };
        }
    }
}
