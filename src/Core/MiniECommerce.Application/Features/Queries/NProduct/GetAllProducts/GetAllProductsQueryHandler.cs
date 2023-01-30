using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Queries.NProduct.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _productReadRepository.GetAll(tracking: false);

            query = query.Include(p => p.Brand);

            // Ürünlerin markalara göre filtrelenmesi;
            string brands = request.Fb;
            if (!string.IsNullOrWhiteSpace(brands))
            {
                if (brands.Contains('-'))
                {
                    foreach (var brandCode in request.Fb.Split("-"))
                    {
                        query = query.Where(p => p.BrandCode.ToString() == brandCode);
                    }
                }
                else
                    query = query.Where(p => p.BrandCode.ToString() == brands);
            }

            var totalProductCount = query.Count();

            // Ürün görselleri isteniyorsa;
            if (request.Wi == 1)
                query = query.Include(p => p.ProductImageFiles);

            // Ürünlerin sıralanması işlemi
            if (!string.IsNullOrWhiteSpace(request.Ob))
            {
                query = request.Ob.ToLower() switch
                {
                    "lowest" => query.OrderBy(p => p.Price),
                    "highest" => query.OrderByDescending(p => p.Price),
                    "newest" => query.OrderByDescending(p => p.CreatedDate),
                    _ => query.OrderByDescending(p => p.CreatedDate),
                };
            }
            else
                query = query.OrderBy(p => p.CreatedDate);

            query = query
                    .Skip(request.Page * request.Size)
                    .Take(request.Size);

            var products = await query
            .Select(p => new
            {
                p.Id,
                p.Name,
                BrandName = p.Brand.Name,
                p.AmountOfStock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate,
                p.ProductImageFiles,
                ImagePath = p.ProductImageFiles.FirstOrDefault().Path
            }).ToListAsync();

            return new()
            {
                TotalProductCount = totalProductCount,
                Products = products
            };

        }
    }
}
