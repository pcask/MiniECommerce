using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.DTOs.NAppUserAddress
{
    public class AppUserAddressDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int NeighborhoodId { get; set; }
        public string AddressDetail { get; set; }
        public string AddressTitle { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Tin { get; set; }
        public string? TaxOffice { get; set; }
        public string? CompanyName { get; set; }
        public bool? IsEInvoicePayer { get; set; }
        public string? AppUserId { get; set; }
    }
}
