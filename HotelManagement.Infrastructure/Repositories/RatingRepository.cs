using HotelManagement.Core;
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
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private readonly HotelDbContext hotelDbContext;
        public RatingRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
            
        }



        public async void RateHotelAsync(Rating rating)
        {
            

            await AddAsync(rating);

        }



    }
}
