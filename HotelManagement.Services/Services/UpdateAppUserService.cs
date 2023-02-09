using AutoMapper;
using HotelManagement.Core;
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
    public class UpdateAppUserService : IUpdateUserAppService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HotelDbContext _hotelDbContext;

        public UpdateAppUserService(IUnitOfWork unitOfWork, IMapper mapper, HotelDbContext hotelDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hotelDbContext = hotelDbContext;
        }

        public async Task<Response<UpdateAppUserDto>> UpdateUser(UpdateAppUserDto update, string Id)
        {

            var updateUser = await _unitOfWork.UpdateAppUserRepository.GetByIdAsync(x => x.Id == Id); 
            updateUser.Id = Id;
            updateUser.FirstName= update.FirstName;
            updateUser.LastName= update.LastName;
            updateUser.Gender= update.Gender;
            updateUser.Age= update.Age;
            await _unitOfWork.UpdateAppUserRepository.UpdateAsync(updateUser);
            

                if (updateUser == null)
                {
                    return new Response<UpdateAppUserDto>
                    {
                        StatusCode = 404,
                        Succeeded = false,
                        Data = null,
                        Message = "User not found"
                    };
                }
                await _unitOfWork.SaveChanges();
                return Response<UpdateAppUserDto>.Success("Updated Successfully", update);
            }
    }
}
