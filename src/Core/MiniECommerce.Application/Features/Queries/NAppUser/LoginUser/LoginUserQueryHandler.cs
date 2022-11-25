using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniECommerce.Application.Abstractions.NToken;
using MiniECommerce.Application.Exceptions;
using MiniECommerce.Domain.Entities.Identity;

namespace MiniECommerce.Application.Features.Queries.NAppUser.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQueryRequest, LoginUserQueryResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserQueryHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserQueryResponse> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                throw new AuthenticationErrorException();


            return new LoginUserQueryResponse()
            {
                Token = _tokenHandler.CreateAccessToken(10)
            };

        }
    }
}
