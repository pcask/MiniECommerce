using MiniECommerce.Application.DTOs;

namespace MiniECommerce.Application.Features.Queries.NAppUser.LoginUser
{
    public class LoginQueryResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
