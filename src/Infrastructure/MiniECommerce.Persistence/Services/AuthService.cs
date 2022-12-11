using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MiniECommerce.Application.Abstractions.FacebookAuthToken;
using MiniECommerce.Application.Abstractions.GoogleIdToken;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.Abstractions.Token;
using MiniECommerce.Application.DTOs;
using MiniECommerce.Application.DTOs.Google;
using MiniECommerce.Application.Exceptions;
using MiniECommerce.Domain.Entities.Identity;

namespace MiniECommerce.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IAppUserService _appUserService;
        private readonly IGoogleIdTokenValidationService _googleIdTokenValidationService;
        private readonly IFacebookAuthTokenValidationService _facebookAuthTokenValidationService;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenHandler tokenHandler,
            IGoogleIdTokenValidationService googleIdTokenValidationService,
            IFacebookAuthTokenValidationService facebookAuthTokenValidationService,
            IAppUserService appUserService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _googleIdTokenValidationService = googleIdTokenValidationService;
            _facebookAuthTokenValidationService = facebookAuthTokenValidationService;
            _appUserService = appUserService;
        }

        public async Task<TokenDto> LoginAsync(string email, string password, int jwtExpireInSecond)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
                throw new AuthenticationErrorException();

            TokenDto token = _tokenHandler.CreateTokens(jwtExpireInSecond, user);
            await _appUserService.UpdateRefreshToken(user, token.RefreshToken, token.RTokenEndDate);
            return token;
        }

        public async Task<TokenDto> LoginWithFacebookAsync(string authToken, int jwtExpireInSecond)
        {
            var userInfoDto = await _facebookAuthTokenValidationService.ValidateAuthTokenAsync(authToken);

            return await ExternalLoginAsync("FACEBOOK", userInfoDto.Id, userInfoDto.Email, jwtExpireInSecond);
        }

        public async Task<TokenDto> LoginWithGoogleAsync(string idToken, int jwtExpireInSecond)
        {
            PayloadDto payload = await _googleIdTokenValidationService.ValidateIdTokenAsync(idToken);

            return await ExternalLoginAsync("GOOGLE", payload.Subject, payload.Email, jwtExpireInSecond);
        }

        private async Task<TokenDto> ExternalLoginAsync(string providerName, string providerKey, string email, int jwtExpireInSecond)
        {
            UserLoginInfo userLoginInfo = new(providerName, providerKey, providerName);

            AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email
                    };

                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                        await _userManager.AddLoginAsync(user, userLoginInfo);
                    else
                        throw new AuthenticationErrorException();
                }
                else
                    await _userManager.AddLoginAsync(user, userLoginInfo);
            }

            TokenDto token = _tokenHandler.CreateTokens(jwtExpireInSecond, user);
            await _appUserService.UpdateRefreshToken(user, token.RefreshToken, token.RTokenEndDate);
            return token;
        }
    }
}
