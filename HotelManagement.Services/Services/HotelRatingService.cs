using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class HotelRatingService : IHotelRatingService
    {

        private readonly IRatingRepository ratingRepository;
        public HotelRatingService(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public Task<bool> RateHotel(int rate)
        {
            var check = false;
            var ratings = ratingRepository.GetAllRating();
            try
            {
                var rating = new Rating();
                rating.Ratings = rate;
                rating.CreatedAt = DateTime.Now;
                rating.UpdatedAt = DateTime.Now;
                
                check = true;
              
            }
            catch (Exception ex)
            {
                
            }
            return true;
            
        }
    }
}
