using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs.BookingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IBookingService
    {
        Task<Response<string>> CreateHotelBooking(BookingRequestDto bookingRequestDto);
        Task<Response<List<BookingResponseDto>>> GetBookingPerManager(string managerId);

    }
}
