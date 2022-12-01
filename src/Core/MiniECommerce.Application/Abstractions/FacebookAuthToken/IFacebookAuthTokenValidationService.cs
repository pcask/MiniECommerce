using MiniECommerce.Application.DTOs.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Abstractions.FacebookAuthToken
{
    public interface IFacebookAuthTokenValidationService
    {
        Task<FbUserInfoDto> ValidateAuthTokenAsync(string authToken);
    }
}
