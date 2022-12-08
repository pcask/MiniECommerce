using MiniECommerce.Application.DTOs;

namespace MiniECommerce.Application.Features.Commands.NAppUser.LoginWithFacebook
{
    public class LoginWithFacebookCommandResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
