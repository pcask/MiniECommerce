using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using MiniECommerce.Application.Abstractions.GoogleIdToken;
using MiniECommerce.Application.DTOs.Google;
using MiniECommerce.Application.Exceptions;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace MiniECommerce.Infrastructure.Services.GoogleIdToken
{
    public class GoogleIdTokenValidationService : IGoogleIdTokenValidationService
    {
        private readonly IConfiguration _configuration;

        public GoogleIdTokenValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PayloadDto> ValidateIdTokenAsync(string idToken)
        {
            try
            {
                ValidationSettings settings = new ValidationSettings()
                {
                    Audience = new List<string>() { _configuration["ExternalLogin:Google-Client-Id"] }
                };

                Payload payload = await ValidateAsync(idToken, settings);

                return new()
                {
                    Subject = payload.Subject,
                    Email = payload.Email,
                    Name = payload.Name,
                };
            }
            catch (Exception)
            {
                throw new InvalidExternalAuthentication();
            }
        }
    }
}
