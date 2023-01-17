using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        //void UpdateAsync(Review review);

        //Task<Review> CheckHotelExistence(string hotelId);
        Review AddReview(string review);

        public IQueryable<Review> GetHotelReviews(string hotelId);
    }
}
