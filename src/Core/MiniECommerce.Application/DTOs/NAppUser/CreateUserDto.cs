using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.DTOs.NAppUser
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
