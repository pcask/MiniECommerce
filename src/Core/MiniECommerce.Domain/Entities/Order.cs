using MiniECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid AppUserAddressId { get; set; }
        public AppUserAddress AppUserAddress { get; set; }

        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
