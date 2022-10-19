using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Repositories;
using MiniECommerce.Domain.Entities.Common;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly MiniECommerceDbContext _context;
        private DbSet<T> DbSet;

        public WriteRepository(MiniECommerceDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => DbSet ??= _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            var entry = await Table.AddAsync(entity);
            return entry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(T entity)
        {
            var entry = Table.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public void RemoveRange(T entities)
            => Table.RemoveRange(entities);

        public bool Update(T entity)
        {
            var entry = Table.Update(entity);
            return entry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
