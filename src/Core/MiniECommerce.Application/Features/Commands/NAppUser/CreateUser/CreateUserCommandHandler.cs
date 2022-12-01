using MediatR;
using MiniECommerce.Application.Abstractions.Services;

namespace MiniECommerce.Application.Features.Commands.NAppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {

        private readonly IAppUserService _appUserService;

        public CreateUserCommandHandler(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            // AutoMapper ile tür döbüşümlerini halledebiliriz.
            var response = await _appUserService.CreateUserAsync(new()
            {
                Email = request.Email,
                Password = request.Password,
                RePassword = request.RePassword
            });

            return new()
            {
                Succeeded = response.Succeeded,
                ErrorMessage = response.ErrorMessage
            };
        }
    }
}
