using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniECommerce.Application.Abstractions.Token;
using MiniECommerce.Application.DTOs;
using MiniECommerce.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MiniECommerce.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;


        private SymmetricSecurityKey _securityKey;
        private string _audience;
        private string _issuer;
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            _audience = _configuration["Token:Audience"];
            _issuer = _configuration["Token:Issuer"];
        }

        public TokenDto CreateTokens(int expireInSeconds, AppUser user)
        {
            TokenDto token = new();

            SigningCredentials signingCredentials = new(_securityKey, SecurityAlgorithms.HmacSha256);

            token.ATokenEndDate = DateTime.UtcNow.AddSeconds(expireInSeconds);

            // Hangi değerlere göre token doğrulaması yapılacağını belirlediysek aynı değerlerle de token'ımızı oluşturuyoruz.
            JwtSecurityToken securityToken = new(
                audience: _audience,
                issuer: _issuer,
                expires: token.ATokenEndDate,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Email, user.Email) }
                );

            token.AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            token.RTokenEndDate = token.ATokenEndDate.AddSeconds(expireInSeconds / 3);
            token.RefreshToken = CreateRefreshToken(token.RTokenEndDate);
            return token;
        }

        public string CreateRefreshToken(DateTime endDate)
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator generator = RandomNumberGenerator.Create();
            generator.GetBytes(number);
            return Convert.ToBase64String(number) + endDate;
        }

        public bool ValidateAccessTokenWithoutExpiration(string accessToken)
        {
            JwtSecurityTokenHandler handler = new();
            handler.ValidateToken(accessToken, new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,

                ValidAudience = _audience,
                ValidIssuer = _issuer,
                IssuerSigningKey = _securityKey
            }, out SecurityToken validatedToken);

            return validatedToken != null;
        }
    }
}
