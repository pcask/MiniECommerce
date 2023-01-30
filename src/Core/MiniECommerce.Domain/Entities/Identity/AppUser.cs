using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<AppUserAddress> AppUserAddresses { get; set; }
    }
}
