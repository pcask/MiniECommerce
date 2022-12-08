using MiniECommerce.Application.DTOs;

namespace MiniECommerce.Application.Features.Commands.NAppUser.LoginWithGoogle
{
    public class LoginWithGoogleCommandResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
