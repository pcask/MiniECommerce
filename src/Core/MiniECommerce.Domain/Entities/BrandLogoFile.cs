using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Domain.Entities
{
    public class BrandLogoFile : File
    {
        public ICollection<Brand> Brands { get; set; }
    }
}
