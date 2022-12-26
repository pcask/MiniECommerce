using MiniECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Domain.Entities
{
    public class Brand : BaseEntity
    {
        public int Code { get; set; }
        public string Name { get; set; }

        public List<BrandLogoFile> BrandLogoFiles { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
