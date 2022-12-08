using Microsoft.AspNetCore.Identity;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.DTOs.NAppUser;
using MiniECommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDto> CreateUserAsync(CreateUserDto model)
        {

            if (model.Password != model.RePassword)
                throw new Exception("Password mismatch");

            IdentityResult result = null;

            var user = await _userManager.FindByEmailAsync(model.Email);

            // Kullanıcı hesap açmadan önce sosyal medyadan giriş yaptıysa ve daha sonra hesap oluşturmak isterse, şifre ataması yapmalıyız;
            if (user != null && user.PasswordHash == null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                result = await _userManager.UpdateAsync(user);
            }
            else
            {
                result = await _userManager.CreateAsync(new()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    Email = model.Email
                }, model.Password);
            }

            CreateUserResponseDto response = new() { Succeeded = result.Succeeded };

            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    response.ErrorMessage += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task UpdateRefreshToken(AppUser user, string refreshToken, DateTime endDate)
        {
            user.RefreshToken = refreshToken.Remove(44);
            user.RefreshTokenEndDate = endDate;
            await _userManager.UpdateAsync(user);
        }
    }
}
