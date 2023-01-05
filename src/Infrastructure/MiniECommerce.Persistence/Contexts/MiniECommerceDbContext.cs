using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Domain.Entities.Common;
using MiniECommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Contexts
{
    public class MiniECommerceDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public MiniECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<BrandLogoFile> BrandLogoFiles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Brand>()
                .Property(b => b.Code)
                .ValueGeneratedOnAdd();

            builder.Entity<Brand>()
                .HasAlternateKey(b => b.Code);

            builder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandCode)
                .HasPrincipalKey(b => b.Code);

            builder.Entity<Cart>()
                .HasOne(c => c.Order)
                .WithOne(o => o.Cart)
                .HasForeignKey<Order>(o => o.CartId);


            base.OnModelCreating(builder);
        }
    }
}
