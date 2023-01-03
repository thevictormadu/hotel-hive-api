using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
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

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Response<string>> AddRoom(string Hotel_ID, AddRoomDto addRoomDto)
        {
            throw new NotImplementedException();
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
    }
}
