﻿using Microsoft.EntityFrameworkCore;

namespace MiniECommerce.Application.DTOs.NAppUserAddress
{
    [Keyless]
    public class DistrictDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
    }
}
