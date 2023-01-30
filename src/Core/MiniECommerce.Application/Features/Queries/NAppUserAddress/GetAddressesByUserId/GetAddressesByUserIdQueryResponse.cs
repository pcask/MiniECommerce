using MiniECommerce.Application.DTOs.NAppUserAddress;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAddressesByUserId
{
    public class GetAddressesByUserIdQueryResponse
    {
        public List<AppUserAddressDto> UserAddresses { get; set; }
    }
}
