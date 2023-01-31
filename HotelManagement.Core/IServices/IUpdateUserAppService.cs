using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IUpdateUserAppService
    {
        Task<Response<UpdateAppUserDto>> UpdateUser(UpdateAppUserDto update, string Id);
    }
}
