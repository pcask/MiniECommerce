﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.ViewModels.Products
{
    public class VM_Create_Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int AmountOfStock { get; set; }
    }
}
