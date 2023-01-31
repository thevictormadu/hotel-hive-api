using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class RoomService:IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HotelDbContext _hotelDbContext;
        private readonly IRoomRepository _roomRepository;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper, HotelDbContext hotelDbContext,IRoomRepository roomRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hotelDbContext = hotelDbContext;
            _roomRepository = roomRepository;
        }

        public async Task<Response<string>> AddRoom(string RoomType_ID, string Hotel_Name, AddRoomDto addRoomDto)
        {
            var hotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Name == Hotel_Name);
            if (hotel == null)
                return new Response<string>
                {
                    Data = Hotel_Name,
                    Succeeded = false,
                    StatusCode = 404,
                    Message = "Hotel Name Not found"
                };
            var roomtype = _hotelDbContext.RoomTypes.Where(x => x.HotelId == hotel.Id);
            var newroom = _mapper.Map<Room>(addRoomDto);
            if (newroom == null) return Response<string>.Fail("Operation Not Successful");
                _unitOfWork.roomRepository.Add(RoomType_ID, newroom);
                _unitOfWork.SaveChanges();
            return Response<string>.Success("Room Created Successfully", newroom.RoomNo);
            
        }

        public async Task<Response<GetRoomDto>> GetRoombyId(string Id)
        {
            try
            {
                var room = await _unitOfWork.roomRepository.GetByIdAsync(x => x.Id == Id);
               
                var data = _mapper.Map<GetRoomDto>(room);
                if (data == null) return Response<GetRoomDto>.Fail("No Room Found");
                return Response<GetRoomDto>.Success(Id, data);
            }
            catch (Exception ex)
            {

                return Response<GetRoomDto>.Fail(ex.Message);
            }
        }

        public async Task<Response<string>> DeleteRoomById(string id)
        {
            try
            {
                var room = _hotelDbContext.Rooms.FirstOrDefault(x => x.Id == id);
                if (room == null)
                    return Response<string>.Fail($"Room with {id} does not exist");
                await _roomRepository.DeleteAsync(room);
                _unitOfWork.SaveChanges();
                return Response<string>.Success($"Room with {id} Sucessful Deleted",id);
                
            }
            catch (Exception ex)
            {

                return Response<string>.Fail(ex.Message);
            };

        }

        public async Task<Response<GetRoomDto>> GetSingleRoom(string Id)
        {
            try
            {
                var SingleRoom = await _unitOfWork.roomRepository.GetByIdAsync(x => x.Id == Id);
                var MapData = _mapper.Map<GetRoomDto>(SingleRoom);
                return MapData == null ? Response<GetRoomDto>.Fail("Room does not Exist") : Response<GetRoomDto>.Success(Id, MapData);
            }
            catch (Exception ex)
            {
                return Response<GetRoomDto>.Fail(ex.Message);
            }
        }
        public async Task<Response<Room>> Create(AddRommDto rommDto)
        {

            var mappedRoom = _mapper.Map<Room>(rommDto);
            if (mappedRoom == null) return Response<Room>.Fail("Operation Not Successful");
            await _unitOfWork.roomRepository.AddAsync(mappedRoom);
            return Response<Room>.Success(" Room Created Successfully", mappedRoom);

        }
    }
}
