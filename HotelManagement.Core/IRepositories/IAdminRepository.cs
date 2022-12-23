using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IAdminRepository
    {
        Task<Response<string>> CreateRole(RoleDTO role);
        Task<Response<string>> AddUserRole(string userId, Roles role);
        Task<Response<string>> RemoveUserRole(string userId, Roles role);
    }
}
