using AutoMapper;
using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.DTOs.BookingDtos;

namespace HotelManagement.Services.Services
{
    public class BookingService : IBookingService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookingService> _logger;
        public BookingService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<BookingService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Response<string>> CreateHotelBooking(BookingRequestDto bookingRequestDto)
        {
            _logger.LogInformation($"Attempt to create hotel bookings for customer with id {bookingRequestDto.CustomerId}");
            var bookingRequest = _mapper.Map<Booking>(bookingRequestDto);
            bookingRequest.Id = Guid.NewGuid().ToString();
            // bookingRequest.CheckIn = DateTime.ParseExact($"{bookingRequest.CheckIn}","MM/dd/yy",CultureInfo.GetCultureInfo("en-NG"));
            // bookingRequest.CheckOut = DateTime.ParseExact($"{bookingRequest.CheckOut}", "MM/dd/yy", CultureInfo.GetCultureInfo("en-NG"));
            try
            {
                await _unitOfWork.bookingRepository.AddAsync(bookingRequest);
                _unitOfWork.SaveChanges();
                return Response<string>.Success("Created Successfully", bookingRequest.Id, statusCode: 200);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the booking");

                // Return a failure response
                return Response<string>.Fail("An error occurred while creating the booking", statusCode: 500);
            }
           
        }
    }
}
