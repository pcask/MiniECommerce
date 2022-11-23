using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniECommerce.Domain.Entities.Identity;

namespace MiniECommerce.Application.Features.Commands.NAppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Email,
                Email = request.Email
            }, request.Password);

            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    response.ErrorMessage += $"{error.Code} - {error.Description}\n";

            return response;
        }
    }
}
