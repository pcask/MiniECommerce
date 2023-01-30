using MiniECommerce.Application.DTOs.NAppUserAddress;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllDistrictsByCityId
{
    public class GetAllDistrictsByCityIdQueryResponse
    {
        public List<DistrictDto> Districts { get; set; }
    }
}
