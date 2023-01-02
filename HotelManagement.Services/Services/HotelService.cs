using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;

namespace HotelManagement.Services.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}
