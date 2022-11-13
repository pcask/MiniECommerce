using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Application.Abstractions.Storage;
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

        public ProductsController(
                IProductReadRepository productReadRepository,
                IProductWriteRepository productWriteRepository,
                IProductImageFileWriteRepository productImageFileWriteRepository,
                IStorageService storageService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(tracking: false).Count();
            var products = _productReadRepository.GetAll(tracking: false)
                .Skip(pagination.Page * pagination.Size)
                .Take(pagination.Size)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.AmountOfStock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate
                }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }


        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                AmountOfStock = model.AmountOfStock,
                Price = model.Price
            });

            await _productWriteRepository.SaveAsync();

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
        public async Task<IActionResult> Upload()
        {
            var datas = await _storageService.UploadAsync("resources/product/images", Request.Form.Files);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
