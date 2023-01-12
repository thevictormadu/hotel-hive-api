using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HotelManagement.Services.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HotelDbContext _hotelDbContext;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper, HotelDbContext hotelDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hotelDbContext = hotelDbContext;
        }

        public async Task<Response<GetHotelsDto>> GetHotelById(string Id)
        {
            var getHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
            var mappedHotel = _mapper.Map<GetHotelsDto>(getHotel);
            if (mappedHotel == null)
            {
                return new Response<GetHotelsDto>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotel not fund"
                };
            }
            return new Response<GetHotelsDto>
            {
                StatusCode = 202,
                Succeeded = true,
                Data = mappedHotel,
                Message = "Successful"
            };
        }

        public async Task<Response<List<GetHotelsDto>>> GetHotels()
        {
            var hotels = await _unitOfWork.hotelRepository.GetAllAsync();
            var allHotels = _mapper.Map<List<GetHotelsDto>>(hotels);
            if (allHotels.Count == 0)
            {
                return new Response<List<GetHotelsDto>>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotels not found"
                };
            }
            return new Response<List<GetHotelsDto>>
            {
                StatusCode = 202,
                Succeeded = true,
                Data = allHotels,
                Message = "Successful"
            };
        }

        public async Task<Response<UpdateHotelDto>> UpdateHotel(UpdateHotelDto update, string Id)
        {
            var updateHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
            var mappedUpdate = _mapper.Map(update, updateHotel);

            if (updateHotel == null)
            {
                return new Response<UpdateHotelDto>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotel not found"
                };
            }
            _unitOfWork.SaveChanges();
            return Response<UpdateHotelDto>.Success("Updated Successfully", update);
        }
        public async Task<Response<List<GetHotelByRatingsDto>>> GetHotelRating(string HotelName)
        {
            try
            {
                var hotelRatings = _unitOfWork.hotelRepository.GetByIdAsync(x => x.Name == HotelName).Result.Ratings;
                var mappedHotelRating = _mapper.Map<List<GetHotelByRatingsDto>>(hotelRatings);

                if (mappedHotelRating == null) return Response<List<GetHotelByRatingsDto>>.Fail($"Hotel with {HotelName} Not Found");
                return Response<List<GetHotelByRatingsDto>>.Success(HotelName, mappedHotelRating);
            }
            catch (Exception ex)
            {

                return Response<List<GetHotelByRatingsDto>>.Fail(ex.Message);
            }
        }

        public async Task<Response<List<GetRoomDto>>> GetRoomsByAvailability(string HotelNmae, string RoomType)
        {
            try
            {
                var roomsByAvailability = _unitOfWork.hotelRepository.GetByIdAsync(x => x.Name == HotelNmae)
                .Result.RoomTypes.Where(x => x.Name == RoomType).SelectMany(x => x.Rooms);
                var rooms = roomsByAvailability.Where(x => x.IsBooked == false).Select(x => x);
                var data = _mapper.Map<List<GetRoomDto>>(rooms);
                if (data == null) return Response<List<GetRoomDto>>.Fail($"{HotelNmae} Has No Room Available For {RoomType} RoomType");
                return Response<List<GetRoomDto>>.Success(HotelNmae, data);
            }
            catch (Exception ex)
            {

                return Response<List<GetRoomDto>>.Fail(ex.Message);
            }
           
        }

         public async Task<Response<GetRoomDto>> GetAvailableRoomsBy(string HotelName, string roomId)
        {
            try
            {
                var room = _unitOfWork.hotelRepository.GetByIdAsync(x => x.Name.ToLower().Trim() == HotelName.ToLower().Trim())
                .Result.RoomTypes.SelectMany(x => x.Rooms).Where(x=>x.IsBooked == false && x.Id == roomId).FirstOrDefault();
                var data = _mapper.Map<GetRoomDto>(room);
                if (data == null) return Response<GetRoomDto>.Fail($"{HotelName} Has No Room Available");
                return Response<GetRoomDto>.Success(HotelName, data);
            }
            catch (Exception ex)
            {

                return Response<GetRoomDto>.Fail($"{HotelName} Has No Room Available");
            }

        }
        public async Task<Response<string>> AddHotel(string Manager_ID, AddHotelDto addHotelDto)
        {
            var manger = await _hotelDbContext.Managers.FirstOrDefaultAsync(x => x.Id == Manager_ID);
            if (manger == null)
                return new Response<string>
                {
                    Data = Manager_ID,
                    Succeeded = false,
                    StatusCode = 404,
                    Message = "Manager Not found"
                };
            var newhotel = _mapper.Map<Hotel>(addHotelDto);
            _unitOfWork.hotelRepository.AddHotel(Manager_ID, newhotel);
            _unitOfWork.SaveChanges();

            return Response<string>.Success("Created Sucessfuly", addHotelDto.Name);
        }

        public async Task<Response<string>> DeleteHotelById(string id)
        {
            try
            {
                var hotelTodelete = _unitOfWork.hotelRepository.DeleteAsync<string>(id);
                if (hotelTodelete == null)
                    return Response<string>.Fail($"Hotel with {id} doesnot exist");
                _unitOfWork.SaveChanges();
                return Response<string>.Success($"Hotel with {id} Sucessful Deleted", id);
 
    }
            catch (Exception ex)
            {

                return Response<string>.Fail(ex.Message);
            };
           
        }

        public async Task<Response<UpdateHotelDto>> PatchHotel(string Id, UpdateHotelDto update)
        {
            try
            {
                var patchHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);

                if (patchHotel == null)
                {
                    return new Response<UpdateHotelDto>
                    {
                        StatusCode = 404,
                        Succeeded = false,
                        Data = null,
                        Message = "Hotel not found"
                    };
                }

                // Update the patchHotel object with the properties from the update object that are not null
                if (update.Name != null) patchHotel.Name = update.Name;
                if (update.State != null) patchHotel.State = update.State;
                if (update.Phone != null) patchHotel.Phone = update.Phone;
                if (update.Email != null) patchHotel.Email = update.Email;
                if (update.Description != null) patchHotel.Description = update.Description;

                _unitOfWork.SaveChanges();

                return Response<UpdateHotelDto>.Success("Hotel updated successfully", update);
            }
            catch (Exception ex)
            {
                return Response<UpdateHotelDto>.Fail(ex.Message);
            }
        }
        public async Task<Response<List<GetHotelByRatingsDto>>> GetHotelByState(string State)
        {
            try
            {
                var Hotels =await  _unitOfWork.hotelRepository.GetAllAsync(x=>x.State.ToLower().Trim()== State.ToLower().Trim());
                var mappedHotels = _mapper.Map<List<GetHotelByRatingsDto>>(Hotels);

                if (mappedHotels == null) return Response<List<GetHotelByRatingsDto>>.Fail($"Hotel Not Found in {State}");
                return Response<List<GetHotelByRatingsDto>>.Success(State, mappedHotels);
            }
            catch (Exception ex)
            {

                return Response<List<GetHotelByRatingsDto>>.Fail(ex.Message);
            }
        }

    }
}

       