﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.DTOs.NAppUserAddress
{
    [Keyless]
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
