using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.DTOs.NAppUserAddress;
using MiniECommerce.Application.Repositories.NAppUserAddress;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAddressesByUserId
{
    public class GetAddressesByUserIdQueryHandler : IRequestHandler<GetAddressesByUserIdQueryRequest, GetAddressesByUserIdQueryResponse>
    {
        private readonly IAppUserAddressReadRepository _appUserAddressReadRepository;

        public GetAddressesByUserIdQueryHandler(IAppUserAddressReadRepository appUserAddressReadRepository)
        {
            _appUserAddressReadRepository = appUserAddressReadRepository;
        }

        public async Task<GetAddressesByUserIdQueryResponse> Handle(GetAddressesByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _appUserAddressReadRepository.GetAll(a => a.AppUserId == request.UserId, tracking: false).Select(a => new AppUserAddressDto
            {
                Id = a.Id.ToString(),
                AppUserId = a.AppUserId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                PhoneNumber = a.PhoneNumber,
                ProvinceId = a.ProvinceId,
                DistrictId = a.DistrictId,
                NeighborhoodId = a.NeighborhoodId,
                AddressDetail = a.AddressDetail,
                AddressTitle = a.AddressTitle,
                CreatedDate = a.CreatedDate,
                Tin = a.Tin,
                CompanyName = a.CompanyName,
                TaxOffice = a.TaxOffice,
                IsEInvoicePayer = a.IsEInvoicePayer
            }).ToListAsync();

            return new() { UserAddresses = response };
        }
    }
}
