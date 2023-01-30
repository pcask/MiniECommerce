using MiniECommerce.Application.DTOs.NAppUserAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Abstractions.Services
{
    public interface IConstantAddressService
    {
        Task<List<CityDto>> GetAllCities();
        Task<List<DistrictDto>> GetAllDistrictsByCityId(int cityId);
        Task<List<NeighborhoodDto>> GetAllNeighborhoodsByDistrictId(int districtId);
    }
}
