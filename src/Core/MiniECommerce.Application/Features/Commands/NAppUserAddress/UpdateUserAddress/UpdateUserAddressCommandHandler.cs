using MediatR;
using MiniECommerce.Application.Repositories.NAppUserAddress;

namespace MiniECommerce.Application.Features.Commands.NAppUserAddress.UpdateUserAddress
{
    public class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommandRequest, UpdateUserAddressCommandResponse>
    {
        private readonly IAppUserAddressReadRepository _appUserAddressReadRepository;
        private readonly IAppUserAddressWriteRepository _appUserAddressWriteRepository;

        public UpdateUserAddressCommandHandler(IAppUserAddressWriteRepository appUserAddressWriteRepository, IAppUserAddressReadRepository appUserAddressReadRepository)
        {
            _appUserAddressWriteRepository = appUserAddressWriteRepository;
            _appUserAddressReadRepository = appUserAddressReadRepository;
        }

        public async Task<UpdateUserAddressCommandResponse> Handle(UpdateUserAddressCommandRequest request, CancellationToken cancellationToken)
        {
            var address = await _appUserAddressReadRepository.GetByIdAsync(request.BeUpdated.Id);

            address.FirstName = request.BeUpdated.FirstName;
            address.LastName = request.BeUpdated.LastName;
            address.PhoneNumber = request.BeUpdated.PhoneNumber;
            address.ProvinceId = request.BeUpdated.ProvinceId;
            address.DistrictId = request.BeUpdated.DistrictId;
            address.NeighborhoodId = request.BeUpdated.NeighborhoodId;
            address.AddressDetail = request.BeUpdated.AddressDetail;
            address.AddressTitle = request.BeUpdated.AddressTitle;
            address.Tin = request.BeUpdated.Tin;
            address.TaxOffice = request.BeUpdated.TaxOffice;
            address.CompanyName = request.BeUpdated.CompanyName;
            address.IsEInvoicePayer = request.BeUpdated.IsEInvoicePayer;

            await _appUserAddressWriteRepository.SaveAsync();

            return new();
        }
    }
}
