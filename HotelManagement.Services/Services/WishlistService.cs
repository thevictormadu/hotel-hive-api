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

namespace HotelManagement.Services.Services
{
    public class WishlistService : IWishlistService 
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WishlistService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<GenericPagination<WishListDto>>> GetWishListAsync(string customerId, GenericPagination<WishListDto> pagination)
        {
            
            var response = await _unitOfWork.wishlist.GetAllAsync(x=>x.CustomerId == customerId);
            

            try
            {
                if (response == null)
                {
                    Response<WishListDto>.Fail("unsuccessful", 400);

                }
                var customerWishlistDto = _mapper.Map<IQueryable<WishListDto>>(response);
                var data = GenericPagination<WishListDto>.ToPagedList(customerWishlistDto, pagination.CurrentPage,pagination.PageSize);
                Response<GenericPagination<WishListDto>>.Success("successful", data, 200);
            }
            catch (Exception ex)
            {
                Response<WishListDto>.Fail("unsuccessful", 400);
            }
            return null;
        }
    }
}
