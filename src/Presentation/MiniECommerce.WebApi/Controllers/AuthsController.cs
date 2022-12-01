using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Application.Features.Commands.NAppUser.LoginWithFacebook;
using MiniECommerce.Application.Features.Commands.NAppUser.LoginWithGoogle;
using MiniECommerce.Application.Features.Queries.NAppUser.LoginUser;

namespace MiniECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginQueryRequest request)
        {
            LoginQueryResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("login-with-google")]
        public async Task<IActionResult> LoginWithGoogle(LoginWithGoogleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("login-with-facebook")]
        public async Task<IActionResult> LoginWithFacebook(LoginWithFacebookCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
