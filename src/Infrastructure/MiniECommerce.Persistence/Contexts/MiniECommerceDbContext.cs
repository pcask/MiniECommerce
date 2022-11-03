using Microsoft.EntityFrameworkCore;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Contexts
{
    public class MiniECommerceDbContext : DbContext
    {
        public MiniECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }


        // Entity'lerimizin bazı property'lerinin otomatik olarak değer atamasını yapmak için merkezi bir yerden müdahele edebiliriz.
        // Böyle bir senaryo için DbContext'in SaveChanges method'ını override ederek bir nevi interceptor yazmış oluyoruz.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
