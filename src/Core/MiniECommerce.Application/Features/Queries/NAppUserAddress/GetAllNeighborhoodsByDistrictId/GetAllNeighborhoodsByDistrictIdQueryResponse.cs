using MiniECommerce.Application.DTOs.NAppUserAddress;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllNeighborhoodsByDistrictId
{
    public class GetAllNeighborhoodsByDistrictIdQueryResponse
    {
        public List<NeighborhoodDto> Neighborhoods{ get; set; }
    }
}
