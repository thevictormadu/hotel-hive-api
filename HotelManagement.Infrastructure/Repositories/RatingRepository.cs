using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly HotelDbContext context;
        public RatingRepository(HotelDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> RateHotel(RateHotelDTO request)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == request.HotelId);
            if (hotel == null)
            {
                return false;
            }
            var rating = new Rating();
            rating.Id = new Guid();
            rating.CreatedAt = DateTime.Now;
            rating.UpdatedAt = DateTime.Now;
            rating.Ratings = request.Rating;
            rating.HotelId = hotel.Id;
            rating.Hotel = hotel;
            await context.SaveChangesAsync();

            return true;          
        }
    }
}
