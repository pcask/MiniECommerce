using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniECommerce.Application.Abstractions.Token;
using MiniECommerce.Application.Exceptions;
using MiniECommerce.Domain.Entities.Identity;

namespace MiniECommerce.Application.Features.Commands.NAppUser.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommandHandler : IRequestHandler<LoginWithRefreshTokenCommandRequest, LoginWithRefreshTokenCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginWithRefreshTokenCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginWithRefreshTokenCommandResponse> Handle(LoginWithRefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            string accessToken = request.AccessToken;
            if (accessToken == null || accessToken == "" || !_tokenHandler.ValidateAccessTokenWithoutExpiration(accessToken))
                throw new AuthenticationErrorException();

            string refreshToken = request.RefreshToken;
            if (refreshToken == null || refreshToken == "")
                throw new AuthenticationErrorException();


            var user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken.Remove(44));

            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                var tokens = _tokenHandler.CreateTokens(30, user);

                user.RefreshToken = tokens.RefreshToken.Remove(44);
                user.RefreshTokenEndDate = tokens.RTokenEndDate;

                await _userManager.UpdateAsync(user);

                return new()
                {
                    AccessToken = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken
                };
            }
            else
                throw new AuthenticationErrorException();
        }
    }
}
