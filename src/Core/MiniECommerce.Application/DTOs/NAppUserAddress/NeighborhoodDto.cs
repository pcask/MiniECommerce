using Microsoft.EntityFrameworkCore;

namespace MiniECommerce.Application.DTOs.NAppUserAddress
{
    [Keyless]
    public class NeighborhoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
