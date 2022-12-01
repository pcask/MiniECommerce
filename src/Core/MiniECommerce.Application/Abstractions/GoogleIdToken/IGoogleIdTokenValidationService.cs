using MiniECommerce.Application.DTOs.Google;

namespace MiniECommerce.Application.Abstractions.GoogleIdToken
{
    public interface IGoogleIdTokenValidationService
    {
        Task<PayloadDto> ValidateIdTokenAsync(string idToken);
    }
}
