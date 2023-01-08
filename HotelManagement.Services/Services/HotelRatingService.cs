using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class HotelRatingService : IHotelRatingService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HotelDbContext _hotelDbContext;

        public HotelRatingService(IUnitOfWork unitOfWork, IMapper mapper, HotelDbContext hotelDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hotelDbContext = hotelDbContext;
        }

        public async Task<Response<string>> RateHotelAsync(string hotelId, string customerId, RateHotelDTO rateHotelDto)
        {
            var hotel = await _hotelDbContext.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
            var customer = await _hotelDbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer == null || hotel == null)
            {
                return new Response<string>
                {
                    Data = hotelId,
                    Succeeded = false,
                    StatusCode = 404,
                    Message = "Hotel Not found"
                };
            }

            var newRating = _mapper.Map<Rating>(rateHotelDto);
            //_unitOfWork.ratingRepository.AddHotel(hotelId, customerId, newRating);
            _unitOfWork.SaveChanges();

            return Response<string>.Success("Created Sucessfuly", rateHotelDto.Rating.ToString());

            
        }
    }
}
