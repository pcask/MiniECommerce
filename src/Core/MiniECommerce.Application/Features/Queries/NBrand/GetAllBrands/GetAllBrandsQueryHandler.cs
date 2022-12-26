using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Repositories.NBrand;

namespace MiniECommerce.Application.Features.Queries.NBrand.GetAllBrands
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQueryRequest, List<GetAllBrandsQueryResponse>>
    {
        private readonly IBrandReadRepository _brandReadRepository;

        public GetAllBrandsQueryHandler(IBrandReadRepository brandReadRepository)
        {
            _brandReadRepository = brandReadRepository;
        }

        public async Task<List<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = _brandReadRepository.GetAll(tracking: false);

            return await brands.Select(b => new GetAllBrandsQueryResponse
            {
                Code = b.Code,
                Name = b.Name
            }).ToListAsync(cancellationToken);
        }
    }
}
