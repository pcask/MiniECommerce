using MediatR;

namespace MiniECommerce.Application.Features.Commands.NAppUser.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommandRequest : IRequest<LoginWithRefreshTokenCommandResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
