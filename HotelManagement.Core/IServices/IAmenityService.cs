using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IAmenityService
    {
        Task<Response<List<AmenityDTO>>> GetAmenities();
        Task<Response<object>> CreateAmenity(CreateAmenitiesDTO createDto);
        Task<List<Amenity>> GetAllAsync(Expression<Func<Amenity, bool>>? filter = null);
        Task<Response<Amenity>> UpdateAmenity(string id, UpdateAmenityDTO updateDto);
        Task<Amenity> GetByIdAsync(Expression<Func<Amenity, bool>>? filter = null, bool tracked = true);
        Task<Response<string>> DeleteAmenity(string id);
        Task<Amenity> GetAsync(Expression<Func<Amenity, bool>>? filter = null, bool tracked = true, string? includeProperties = null);

    }
}
