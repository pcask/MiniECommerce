using MiniECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int AmountOfStock { get; set; }
        public double Price { get; set; }

        public int BrandCode { get; set; }
        public Brand Brand { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
