using MiniECommerce.Domain.Entities.Common;
using MiniECommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Domain.Entities
{
    public class AppUserAddress : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int NeighborhoodId { get; set; }
        public string AddressDetail { get; set; }
        public string AddressTitle { get; set; }
        public string? Tin { get; set; } // tax identification number (vergi kimlik numarası)
        public string? TaxOffice { get; set; }
        public string? CompanyName { get; set; }
        public bool? IsEInvoicePayer { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
