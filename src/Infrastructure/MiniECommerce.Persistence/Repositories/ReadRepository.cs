using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Repositories;
using MiniECommerce.Domain.Entities.Common;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly MiniECommerceDbContext _context;
        private DbSet<T> DbSet;

        public ReadRepository(MiniECommerceDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => DbSet ??= _context.Set<T>();

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, bool tracking = true)
        {
            if (filter != null)
            {
                if (!tracking)
                    return Table.Where(filter).AsNoTracking();
                else
                    return Table.Where(filter);
            }
            else
            {
                if (!tracking)
                    return Table.AsNoTracking();
                else
                    return Table;
            }

        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(e => e.Id == Guid.Parse(id));
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(filter);
        }
    }
}
