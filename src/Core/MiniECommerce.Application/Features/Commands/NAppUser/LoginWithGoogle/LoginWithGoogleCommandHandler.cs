using MediatR;
using MiniECommerce.Application.Abstractions.GoogleIdToken;

namespace MiniECommerce.Application.Features.Commands.NAppUser.LoginWithGoogle
{
    public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommandRequest, LoginWithGoogleCommandResponse>
    {
        private readonly IGoogleIdTokenValidationService _idTokenValidationService;

        public LoginWithGoogleCommandHandler(IGoogleIdTokenValidationService idTokenValidationService)
        {
            _idTokenValidationService = idTokenValidationService;
        }

        public async Task<LoginWithGoogleCommandResponse> Handle(LoginWithGoogleCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _idTokenValidationService.ValidateIdTokenAsync(request);

            return new() { Token = response };
        }
    }
}
