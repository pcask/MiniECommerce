﻿using Microsoft.EntityFrameworkCore;
using MiniECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public DbSet<T> Table { get; }
    }
}
