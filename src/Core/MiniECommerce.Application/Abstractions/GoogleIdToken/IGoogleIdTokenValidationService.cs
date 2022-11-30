using MiniECommerce.Application.DTOs;
using MiniECommerce.Application.Features.Commands.NAppUser.LoginWithGoogle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Abstractions.GoogleIdToken
{
    public interface IGoogleIdTokenValidationService
    {
        Task<Token> ValidateIdTokenAsync(LoginWithGoogleCommandRequest request);
    }
}
