﻿using MiniECommerce.Application.DTOs.NAppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Abstractions.Services
{
    public interface IAppUserService
    {
        Task<CreateUserResponseDto> CreateUserAsync(CreateUserDto model);
    }
}
