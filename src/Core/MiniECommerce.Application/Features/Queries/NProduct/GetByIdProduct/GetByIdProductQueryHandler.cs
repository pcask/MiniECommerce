using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Domain.Entities;

namespace MiniECommerce.Application.Features.Queries.NProduct.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _productReadRepository.Table.Include(p => p.Brand).Include(p => p.ProductImageFiles);

            // Sorgu (IQueryable nesnesi) son evresine ulaştıktan sonra, Select atarak veri isteği yapmak (toList, first) 
            // bizi infinite loop'dan kurtaracaktır. Eğer ki öncelikli olarak veri isteği yapıp, ardından data'yı select
            // işlemine tabi tutsaydık Entity'ler arasında navigation property'ler olduğu için infinite loop'a girmiş ve 
            // bu tarz bir hata almış olucaktık. "A possible object cycle was detected."
            var product = await query.Select(p => new
            {
                p.Id,
                p.Name,
                BrandName = p.Brand.Name,
                p.BrandCode,
                p.Price,
                p.AmountOfStock,
                p.ProductImageFiles,
                p.CreatedDate,
                p.UpdatedDate
            }).FirstOrDefaultAsync(p => p.Id.ToString() == request.Id);

            return new()
            {
                Product = product
            };

        }
    }
}
