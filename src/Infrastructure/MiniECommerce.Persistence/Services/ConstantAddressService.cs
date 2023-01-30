using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.DTOs.NAppUserAddress;
using MiniECommerce.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Services
{
    public class ConstantAddressService : IConstantAddressService
    {
        private readonly MiniECommerceDbContext _context;

        public ConstantAddressService(MiniECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<CityDto>> GetAllCities()
        {
            return await _context.Cities.FromSqlRaw("select id, il_adi as name from iller").ToListAsync();
        }

        public async Task<List<DistrictDto>> GetAllDistrictsByCityId(int cityId)
        {
            return await _context.Districts.FromSqlRaw($"select id, ilce_adi as name, il_id as cityId from ilceler where il_id = {cityId}").ToListAsync();
        }

        public async Task<List<NeighborhoodDto>> GetAllNeighborhoodsByDistrictId(int districtId)
        {
            return await _context.Neighborhoods
            .FromSqlRaw($"select id, mahalle_adi as name, posta_kodu as zipCode from mahalleler where semt_id in(select id from semtler where ilce_id = {districtId})")
            .ToListAsync();
        }
    }
}
