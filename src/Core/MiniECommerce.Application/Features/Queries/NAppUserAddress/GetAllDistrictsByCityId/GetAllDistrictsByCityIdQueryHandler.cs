using MediatR;
using MiniECommerce.Application.Abstractions.Services;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllDistrictsByCityId
{
    public class GetAllDistrictsByCityIdQueryHandler : IRequestHandler<GetAllDistrictsByCityIdQueryRequest, GetAllDistrictsByCityIdQueryResponse>
    {
        private readonly IConstantAddressService _constantAddressService;

        public GetAllDistrictsByCityIdQueryHandler(IConstantAddressService constantAddressService)
        {
            _constantAddressService = constantAddressService;
        }

        public async Task<GetAllDistrictsByCityIdQueryResponse> Handle(GetAllDistrictsByCityIdQueryRequest request, CancellationToken cancellationToken)
        {
            var districts = await _constantAddressService.GetAllDistrictsByCityId(request.CityId);

            return new() { Districts = districts };
        }
    }
}
