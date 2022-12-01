using Microsoft.Extensions.Configuration;
using MiniECommerce.Application.Abstractions.FacebookAuthToken;
using MiniECommerce.Application.DTOs.Facebook;
using MiniECommerce.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services.FacebookAuthToken
{
    public class FacebookAuthTokenValidationService : IFacebookAuthTokenValidationService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public FacebookAuthTokenValidationService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<FbUserInfoDto> ValidateAuthTokenAsync(string authToken)
        {
            //Generate accessToken with your App's Id and Secret
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?" +
                $"client_id={_configuration["ExternalLogin:Facebook:App-Id"]}&" +
                $"client_secret={_configuration["ExternalLogin:Facebook:App-Secret"]}&" +
                $"grant_type=client_credentials");

            FbAccessTokenDto accessTokenDto = JsonSerializer.Deserialize<FbAccessTokenDto>(accessTokenResponse);

            // Validate user session token(request.AuthToken) with accessToken
            string accessTokenValidationResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?" +
                $"input_token={authToken}&" +
                $"access_token={accessTokenDto?.AccessToken}");

            FbAccessTokenValidationDto validationDto = JsonSerializer.Deserialize<FbAccessTokenValidationDto>(accessTokenValidationResponse);

            if (validationDto == null ? true : !validationDto.Data.IsValid)
                throw new InvalidExternalAuthentication();

            string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?" +
                $"fields=email,name&" +
                $"access_token={authToken}");

            return JsonSerializer.Deserialize<FbUserInfoDto>(userInfoResponse);
        }
    }
}
