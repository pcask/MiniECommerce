using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Abstractions.Storage;
using MiniECommerce.Application.Features.Commands.CreateProduct;
using MiniECommerce.Application.Features.Queries.GetAllProducts;
using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Application.Repositories.NProductImageFile;
using MiniECommerce.Application.RequestParameters;
using MiniECommerce.Application.ViewModels.Products;
using MiniECommerce.Domain.Entities;
using System.Net;

namespace MiniECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;

        public ProductsController(
                IProductReadRepository productReadRepository,
                IProductWriteRepository productWriteRepository,
                IProductImageFileWriteRepository productImageFileWriteRepository,
                IStorageService storageService,
                IConfiguration configuration,
                IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryRequest getAllProductsQueryRequest)
        {
            GetAllProductsQueryResponse response = await _mediator.Send(getAllProductsQueryRequest);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetAsync(p => p.Id.ToString() == model.Id);
            product.Name = model.Name;
            product.AmountOfStock = model.AmountOfStock;
            product.Price = model.Price;

            await _productWriteRepository.SaveAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            var datas = await _storageService.UploadAsync("resources/product/images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImages(string id)
        {
            Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));


            return Ok(product.ProductImageFiles.Select(i => new
            {
                i.Id,
                i.FileName,
                Path = i.Path = $"{_configuration["StorageBaseURL"]}/{i.FileName}",
                i.CreatedDate
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteImage(string id, string imageId)
        {
            Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            product.ProductImageFiles.Remove(product.ProductImageFiles.FirstOrDefault(i => i.Id == Guid.Parse(imageId)));

            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
