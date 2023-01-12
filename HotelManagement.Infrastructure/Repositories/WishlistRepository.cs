using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class WishlistRepository : GenericRepository<WishList>, IWishlistRepository
    {
        

        public WishlistRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
            
        }
        
    }

   
}
