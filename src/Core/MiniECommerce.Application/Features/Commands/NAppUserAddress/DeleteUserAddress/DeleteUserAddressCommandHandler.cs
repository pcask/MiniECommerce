using MediatR;
using MiniECommerce.Application.Repositories.NAppUserAddress;

namespace MiniECommerce.Application.Features.Commands.NAppUserAddress.DeleteUserAddress
{
    public class DeleteUserAddressCommandHandler : IRequestHandler<DeleteUserAddressCommandRequest, DeleteUserAddressCommandResponse>
    {
        private readonly IAppUserAddressWriteRepository _appUserAddressWriteRepository;

        public DeleteUserAddressCommandHandler(IAppUserAddressWriteRepository appUserAddressWriteRepository)
        {
            _appUserAddressWriteRepository = appUserAddressWriteRepository;
        }

        public async Task<DeleteUserAddressCommandResponse> Handle(DeleteUserAddressCommandRequest request, CancellationToken cancellationToken)
        {
            await _appUserAddressWriteRepository.RemoveAsync(request.AddressId);
            await _appUserAddressWriteRepository.SaveAsync();

            return new();
        }
    }
}
