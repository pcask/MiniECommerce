using MiniECommerce.Application.DTOs;

namespace MiniECommerce.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<TokenDto> LoginWithGoogleAsync(string idToken, int jwtExpireInSecond);
        Task<TokenDto> LoginWithFacebookAsync(string authToken, int jwtExpireInSecond);
    }
}
