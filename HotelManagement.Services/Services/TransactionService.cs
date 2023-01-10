using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _hotelDbContext;
        protected DbSet<Payment> _dbSet;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IMapper mapper, HotelDbContext hotelDbContext, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _hotelDbContext = hotelDbContext;
            _dbSet = hotelDbContext.Set<Payment>();
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Response<List<RoomTransactionDTO>>> GetRoomTransactionsByManger(string managerId)
        {
           

         
                // Find the manager with the specified ID and include the hotels, rooms, room types, and bookings associated with the manager
                var manager = await _hotelDbContext.Managers
                    .Include(m => m.Hotels)
                        .ThenInclude(h => h.RoomTypes)
                    .Include(m => m.Hotels)
                        .ThenInclude(h => h.Bookings)
                    .FirstOrDefaultAsync(m => m.Id == managerId);

                if (manager == null)
                {
                    return Response<List<RoomTransactionDTO>>.Fail("Manager not found.", 404);
                }

                // Create a list to store the room transaction DTOs
                var roomTransactionDtos = new List<RoomTransactionDTO>();

                // Loop through each hotel associated with the manager
                foreach (var hotel in manager.Hotels)
                {
                    // Loop through each room in the hotel
                    foreach (var room in hotel.Rooms)
                    {
                        // Find the room type for the current room
                        var roomType = hotel.RoomTypes.FirstOrDefault(rt => rt.Id == room.RoomTypeId);

                        // Find the booking for the current room (if it exists)
                        var booking = hotel.Bookings.FirstOrDefault(b => b.RoomTypeId == room.RoomTypeId);

                        // Create a new room transaction DTO for the current room
                        var roomTransactionDto = new RoomTransactionDTO
                        {
                            HotelName = hotel.Name,
                            RoomNo = room.RoomNo,
                            RoomType = roomType.Name,
                            Price = roomType.Price,
                            Discount = roomType.Discount,
                            IsBooked = room.IsBooked,
                        };

                        // If there is a booking for the current room, add the booking details to the room transaction DTO
                        if (booking != null)
                        {
                            roomTransactionDto.BookingId = booking.Id;
                            roomTransactionDto.CheckInDate = booking.CheckIn;
                            roomTransactionDto.CheckOutDate = booking.CheckOut;
                        }

                        // Add the room transaction DTO to the list
                        roomTransactionDtos.Add(roomTransactionDto);
                    }
                }

                return Response<List<RoomTransactionDTO>>.Success("Room transactions retrieved successfully.", roomTransactionDtos, 200);
            }

        public async  Task<Response<List<RoomTransactionDTO>>> GetAllRoomsTransactions(string hotelId)
        {
            // Find the hotel with the specified ID and include the rooms, room types, and bookings associated with the hotel
            var hotel = await _hotelDbContext.Hotels
                .Include(h => h.Rooms)
                .Include(h => h.RoomTypes)
                .Include(h => h.Bookings)
                .FirstOrDefaultAsync(h => h.Id == hotelId);

            //check if tyhe hotelid is null
            if (hotel == null)
            {
                return Response<List<RoomTransactionDTO>>.Fail("Hotel not found.", 404);
            }
            // Create a list to store the room transaction DTOs
            var roomTransactionDtos = new List<RoomTransactionDTO>();
            // Loop through each room in the hotel
            foreach (var room in hotel.Rooms)
            {
                // Find the room type for the current room
                var roomType = hotel.RoomTypes.FirstOrDefault(rt => rt.Id == room.RoomTypeId);

                // Find the booking for the current roomType (if it exists)
                var booking = hotel.Bookings.FirstOrDefault(b => b.RoomTypeId == room.RoomTypeId);

                // Create a new room transaction DTO for the current room
                var roomTransactionDto = new RoomTransactionDTO
                {
                    HotelName = hotel.Name,
                    RoomNo = room.RoomNo,
                    RoomType = roomType.Name,
                    Price = roomType.Price,
                    Discount = roomType.Discount,
                    IsBooked = room.IsBooked,
                };

                // If there is a booking for the current room, add the booking details to the room transaction DTO
                if (booking != null)
                {
                    roomTransactionDto.BookingId = booking.Id;
                    roomTransactionDto.CheckInDate = booking.CheckIn;
                    roomTransactionDto.CheckOutDate = booking.CheckOut;
                }
                // Add the room transaction DTO to the list
                roomTransactionDtos.Add(roomTransactionDto);
            }

            return Response<List<RoomTransactionDTO>>.Success("Room transactions retrieved successfully.", roomTransactionDtos, 200);
        }

    }
        }
    

