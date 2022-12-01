using MiniECommerce.Application.Abstractions.Services.Authentication;

namespace MiniECommerce.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {

    }
}
