using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.DTOs.NAppUser
{
    public class CreateUserResponseDto
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
