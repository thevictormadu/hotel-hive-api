using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.IServices;
using HotelManagement.Core.DTOs;
using AutoMapper;
using HotelManagement.Application.Utility;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Services.Services
{
    public class WishlistService : IWishlistService 
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingService> _logger;

        public WishlistService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BookingService> logger)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<GenericPagination<WishListDto>>> GetWishListAsync(string customerId, int pageNumber, int pageSize)
        {
            //get wishlist of a particular customer
            var response = await _unitOfWork.wishlist.GetByIdAsync(x=>x.CustomerId == customerId);
            

            try
            {
                if (response == null)
                {
                    // checks if the customer's wishlist is empty
                  return  Response<GenericPagination<WishListDto>>.Fail("no wishlist for this customer", 400);

                }
                //maps wishlist response with the WishlistDto
                var customerWishlistDto = _mapper.Map<IQueryable<WishListDto>>(response);

                //paginates wishlist response
                var data = GenericPagination<WishListDto>.ToPagedList(customerWishlistDto, pageNumber, pageSize);

                //returns the paginated wishlist of the customer with {customerId}
                return Response<GenericPagination<WishListDto>>.Success("customer's wishlist fetched", data, 200);
            }
            catch (Exception ex)
            {
              return  Response<GenericPagination<WishListDto>>.Fail(ex.Message, 400);
            }
            return null;
        }
    }
}
