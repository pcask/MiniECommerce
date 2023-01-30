using MediatR;
using MiniECommerce.Application.Abstractions.Services;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllNeighborhoodsByDistrictId
{
    public class GetAllNeighborhoodsByDistrictIdQueryHandler : IRequestHandler<GetAllNeighborhoodsByDistrictIdQueryRequest, GetAllNeighborhoodsByDistrictIdQueryResponse>
    {
        private readonly IConstantAddressService _constantAddressService;

        public GetAllNeighborhoodsByDistrictIdQueryHandler(IConstantAddressService constantAddressService)
        {
            _constantAddressService = constantAddressService;
        }

        public async Task<GetAllNeighborhoodsByDistrictIdQueryResponse> Handle(GetAllNeighborhoodsByDistrictIdQueryRequest request, CancellationToken cancellationToken)
        {
            var neighborhoods = await _constantAddressService.GetAllNeighborhoodsByDistrictId(request.DistrictId);

            return new() { Neighborhoods = neighborhoods };
        }
    }
}
