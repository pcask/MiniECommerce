using MiniECommerce.Application.Repositories.NBrandLogoFile;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Repositories.NBrandLogoFile
{
    public class BrandLogoFileReadRepository : ReadRepository<BrandLogoFile>, IBrandLogoFileReadRepository
    {
        public BrandLogoFileReadRepository(MiniECommerceDbContext context) : base(context)
        {
        }
    }
}
