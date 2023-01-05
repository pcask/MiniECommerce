using MiniECommerce.Domain.Entities.Common;
using MiniECommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; }

        public AppUser User { get; set; }
        public Order Order { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
