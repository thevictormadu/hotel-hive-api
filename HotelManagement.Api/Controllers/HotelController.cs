using AutoMapper;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("Hotel-Rating")]
        public async Task<IActionResult> GetHotelRating([FromQuery] string HotelName)
        {

            var result = await _unitOfWork.hotel.GetHotelsRating(HotelName);
            if (result == null)
                return StatusCode(400, new APIResponse<List<ICollection<Rating>>>
                {
                    StatusCode = HttpStatusCode.NoContent,
                    IsSuccess = false,
                    Result = result,
                });
            _unitOfWork.Save();

            var mappedResult = _mapper.Map<List<ICollection<GetHotelByRatingsDto>>>(result);
            return StatusCode(200, new APIResponse<List<ICollection<GetHotelByRatingsDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = mappedResult,
                ErrorMessages = null
            });
        }
        [HttpGet("Hotel-Room-By-Availability")]
        public async Task<IActionResult> GetRoomByAvailability([FromQuery] string HotelName, String roomType)
        {
            var result = await _unitOfWork.hotel.GetRoomsByAvailability(HotelName, roomType);
            if (result == null)
                return StatusCode(400, new APIResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Result = result
                });
            _unitOfWork.Save();
            var mappedResult = _mapper.Map<IEnumerable<GetRoomDto>>(result);
            return StatusCode(200, new APIResponse<IEnumerable<GetRoomDto>>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = mappedResult,
                ErrorMessages = null
            });
        }

        [HttpGet("Room-By-Id")]
        public async Task<IActionResult> GetRoomById(string Id)
        {
            var result = await _unitOfWork.hotel.GetHotelRoomByRoomId(Id);
            if (result == null)
                return StatusCode(400, new APIResponse<Room>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Result = result
                });
            _unitOfWork.Save();
            var mappedResult = _mapper.Map<GetRoomDto>(result);
            return StatusCode(201, new APIResponse<GetRoomDto>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = false,
                Result = mappedResult,
                ErrorMessages = null
            });
        }

    }
}
