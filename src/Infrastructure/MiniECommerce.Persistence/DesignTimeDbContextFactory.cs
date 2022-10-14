using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence
{
    // Package Manager Console vasıtasıyla migration oluşturulurken IoC Container'a eklediğimiz MiniECommerceDbContext okunarak migrationlar
    // oluşturuluyor. Fakat dotnet ef CLI üzerinden migration'ların oluşturulmasını istersek IoC Container' a gidilemeyeceğinden
    // (Visual Studio ide'sinin olmadığı ortamlarda)
    // DbContext'imizi açıkca belirtmemiz gerekiyor. Böyle bir senaryoda aşağıdaki gibi bir class işimizi görecektir.
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MiniECommerceDbContext>
    {
        public MiniECommerceDbContext CreateDbContext(string[] args)
        {
            // Presentation katmanındaki appsettings.josn dosyasında ki connectionString'i okumak için;
            // - Microsoft.Extensions.Configuration
            // - Microsoft.Extensions.Configuration.Json
            // paketleri yüklenerek ilgili sınıf ve method'lara ulaşılabilir.
            ConfigurationManager configuration = new();
            configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MiniECommerce.WebApi"));
            configuration.AddJsonFile("appsettings.json");

            return new(new DbContextOptionsBuilder<MiniECommerceDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Npgsql"))
                .Options);
        }
    }
}
