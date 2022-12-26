using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Domain.Entities
{
    public class InvoiceFile : File
    {
        public double Price { get; set; }

        [NotMapped]
        public override bool Showcase { get => base.Showcase; set => base.Showcase = value; }
    }
}
