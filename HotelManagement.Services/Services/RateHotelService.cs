using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class RateHotelService : IRateHotelService
    {
        private readonly IRateHotelRepository _rateHotelRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RateHotelService(IRateHotelRepository rateHotelRepository, IHotelRepository hotelRepository, ICustomerRepository customerRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _rateHotelRepository = rateHotelRepository;
            _hotelRepository = hotelRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<string>> RateHotel(RateHotelDTO rateHotelDto)
        {
            var hotel = await _hotelRepository.GetByIdAsync(x => x.Id == rateHotelDto.HotelId);
            if (hotel == null)
            {
                return new Response<string>
                {
                    Data = rateHotelDto.HotelId,
                    Succeeded = false,
                    Message = "Hotel Not Found",
                    StatusCode = 404
                };
            }

            var customer = await _customerRepository.GetByIdAsync(x => x.Id == rateHotelDto.CustomerId);
            if (customer == null)
            {
                return new Response<string>
                {
                    Data = rateHotelDto.CustomerId,
                    Succeeded = false,
                    Message = "Customer Not Found",
                    StatusCode = 404
                };
            }

            var newRating = new Rating()
            {
                Ratings = rateHotelDto.Rating,
                CustomerId = rateHotelDto.CustomerId,
                Customer = customer,
                HotelId = rateHotelDto.HotelId,
                Hotel = hotel
            };

            await _unitOfWork.rateHotelRepository.RateHotel(newRating);
            //_unitOfWork.SaveChangesAsync();

            return Response<string>.Success("Rated Successfully", hotel.Name);

            //var newRating = _mapper.Map<Rating>(rateHotelDto);
            //newRating.Customer = customer;
            //newRating.Hotel = hotel;
            //_unitOfWork.RatingRepository.RateHotelAsync(newRating);
            //_unitOfWork.SaveChanges();

        }
    }
}
