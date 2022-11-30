using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Application.Features.Commands.NAppUser.CreateUser;
using MiniECommerce.Application.Features.Commands.NAppUser.LoginWithGoogle;
using MiniECommerce.Application.Features.Queries.NAppUser.LoginUser;

namespace MiniECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser(LoginUserQueryRequest request)
        {
            LoginUserQueryResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("login-with-google")]
        public async Task<IActionResult> LoginWithGoogle(LoginWithGoogleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
