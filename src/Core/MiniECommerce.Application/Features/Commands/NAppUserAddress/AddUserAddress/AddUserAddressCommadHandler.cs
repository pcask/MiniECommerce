using MediatR;
using MiniECommerce.Application.Repositories.NAppUserAddress;

namespace MiniECommerce.Application.Features.Commands.NAppUserAddress.AddUserAddress
{
    public class AddUserAddressCommadHandler : IRequestHandler<AddUserAddressCommandRequest, AddUserAddressCommandResponse>
    {
        private readonly IAppUserAddressWriteRepository _appUserAddressWriteRepository;

        public AddUserAddressCommadHandler(IAppUserAddressWriteRepository appUserAddressWriteRepository)
        {
            _appUserAddressWriteRepository = appUserAddressWriteRepository;
        }

        public async Task<AddUserAddressCommandResponse> Handle(AddUserAddressCommandRequest request, CancellationToken cancellationToken)
        {
            await _appUserAddressWriteRepository.AddAsync(new()
            {
                AppUserId = request.BeCreated.AppUserId,
                FirstName = request.BeCreated.FirstName,
                LastName = request.BeCreated.LastName,
                PhoneNumber = request.BeCreated.PhoneNumber,
                ProvinceId = request.BeCreated.ProvinceId,
                DistrictId = request.BeCreated.DistrictId,
                NeighborhoodId = request.BeCreated.NeighborhoodId,
                AddressDetail = request.BeCreated.AddressDetail,
                AddressTitle = request.BeCreated.AddressTitle,
                CompanyName = request.BeCreated.CompanyName,
                TaxOffice = request.BeCreated.TaxOffice,
                Tin = request.BeCreated.Tin,
                IsEInvoicePayer = request.BeCreated.IsEInvoicePayer
            });

            await _appUserAddressWriteRepository.SaveAsync();

            return new();
        }
    }
}
