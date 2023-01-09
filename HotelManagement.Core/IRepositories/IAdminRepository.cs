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
        Task<bool> CreateRole(RoleDTO role);
        Task<bool> AddUserRole(string userId, Roles role);
        Task<bool> RemoveUserRole(string userId, Roles role);
    }
}
