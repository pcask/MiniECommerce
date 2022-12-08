using MiniECommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        TokenDto CreateTokens(int expireInSeconds);
        string CreateRefreshToken(DateTime endDate);

        bool ValidateAccessTokenWithoutExpiration(string accessToken);
    }
}
