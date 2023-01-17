using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly HotelDbContext _context;
        
        public ReviewRepository(HotelDbContext context) : base(context)
        {
            _context = context;
            

        }
        //public async Task<Review> CheckHotelExistence(string hotelId)
        //{
        //    return await _reviews.Where(x => x.HotelId == hotelId).FirstOrDefaultAsync();

        //}

        public Review AddReview(string hotelId)
        {
            var customerReview = _context.Reviews.SingleOrDefault(x => x.Id == hotelId);
            return customerReview;
        }

        public IQueryable<Review> GetHotelReviews(string hotelId)
        {
            var query = _context.Reviews.AsNoTracking()
                .Where(x => x.HotelId == hotelId)
                .Include(x => x.Hotel)
                .Include(x => x.Customer.AppUser)
                .OrderBy(x => x.CreatedAt);

            return query;

        }

    }
}
 