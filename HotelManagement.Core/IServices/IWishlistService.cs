using HotelManagement.Application.Utility;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IWishlistService
    {
        Task<Response<GenericPagination<WishListDto>>> GetWishListAsync(string customerId, int pageNumber, int pageSize);
    }
}
