using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MiniECommerce.Application.Abstractions.GoogleIdToken;
using MiniECommerce.Application.Abstractions.NToken;
using MiniECommerce.Application.DTOs;
using MiniECommerce.Application.Features.Commands.NAppUser.LoginWithGoogle;
using MiniECommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace MiniECommerce.Infrastructure.Services.GoogleIdToken
{
    public class GoogleIdTokenValidationService : IGoogleIdTokenValidationService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public GoogleIdTokenValidationService(IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> ValidateIdTokenAsync(LoginWithGoogleCommandRequest request)
        {
            ValidationSettings? settings = new ValidationSettings()
            {
                Audience = new List<string>() { _configuration["ExternalLogin:Google-Client-Id"] }
            };

            Payload payload = await ValidateAsync(request.IdToken, settings);

            UserLoginInfo userLoginInfo = new(request.Provider, payload.Subject, request.Provider);

            AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = request.Email,
                        UserName = request.Email
                    };

                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                        await _userManager.AddLoginAsync(user, userLoginInfo);
                    else
                        throw new Exception("Invalid external authentication");
                }
                else
                    await _userManager.AddLoginAsync(user, userLoginInfo);
            }

            Token token = _tokenHandler.CreateAccessToken(10);

            return token;
        }
    }
}
