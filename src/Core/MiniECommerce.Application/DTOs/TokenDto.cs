﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.DTOs
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime ATokenEndDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RTokenEndDate { get; set; }
    }
}
