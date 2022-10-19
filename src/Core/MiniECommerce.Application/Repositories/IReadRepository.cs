﻿using MiniECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, bool tracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
