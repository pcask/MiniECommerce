using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Application.Features.Queries.NBrand.GetAllBrands;
using MiniECommerce.Application.Features.Queries.NBrand.GetBrandByCode;

namespace MiniECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllBrandsQueryRequest request = new();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]/{ProductId}")]
        public async Task<IActionResult> GetBrandByProductId([FromRoute] GetBrandByProductIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
