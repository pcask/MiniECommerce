﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Application.Features.Commands.NProduct.CreateProduct;
using MiniECommerce.Application.Features.Commands.NProduct.DeleteProduct;
using MiniECommerce.Application.Features.Commands.NProduct.DeleteProductImage;
using MiniECommerce.Application.Features.Commands.NProduct.UpdateProduct;
using MiniECommerce.Application.Features.Commands.NProductImageFile.SetImageShowcase;
using MiniECommerce.Application.Features.Commands.NProductImageFile.UploadProductImageFile;
using MiniECommerce.Application.Features.Queries.NProduct.GetAllProducts;
using MiniECommerce.Application.Features.Queries.NProduct.GetByIdProduct;
using MiniECommerce.Application.Features.Queries.NProduct.GetProductImages;
using System.Net;

namespace MiniECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryRequest getAllProductsQueryRequest)
        {
            GetAllProductsQueryResponse response = await _mediator.Send(getAllProductsQueryRequest);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommandRequest request)
        {
            await _mediator.Send(request);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageFileCommandRequest request)
        {
            request.Files = Request.Form.Files;

            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImages([FromRoute] GetProductImagesQueryRequest request)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteImage(string id, string imageId)
        {
            DeleteProductImageCommandRequest request = new() { Id = id, ImageId = imageId };
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> SetImageShowcase([FromBody] SetImageShowcaseCommandRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
