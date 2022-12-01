using MiniECommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task<TokenDto> LoginAsync(string email, string password, int second);
    }
}
