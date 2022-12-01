using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.DTOs.Google
{
    public class PayloadDto
    {
        // Gets or sets subject claim identifying the principal that is the subject of the JWT or null.
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
