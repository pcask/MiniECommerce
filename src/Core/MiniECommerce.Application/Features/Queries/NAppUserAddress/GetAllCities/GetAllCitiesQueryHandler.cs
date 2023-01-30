using MediatR;
using MiniECommerce.Application.Abstractions.Services;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllCities
{
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQueryRequest, GetAllCitiesQueryResponse>
    {
        private readonly IConstantAddressService _constantAddressService;

        public GetAllCitiesQueryHandler(IConstantAddressService constantAddressService)
        {
            _constantAddressService = constantAddressService;
        }

        public async Task<GetAllCitiesQueryResponse> Handle(GetAllCitiesQueryRequest request, CancellationToken cancellationToken)
        {
            var cities = await _constantAddressService.GetAllCities();

            return new() { Cities = cities };
        }
    }
}
