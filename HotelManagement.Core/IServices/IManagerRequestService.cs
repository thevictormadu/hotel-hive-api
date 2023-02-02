using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IManagerRequestService
    {
        Task<Response<string>> ManagerRequest(ManagerRequestDTO managerRequest);
        Task<Response<string>> AdminSendInvite(string requestId);
        Task<Response<string>> ManagerAcceptInvite(string token);
        Task<Response<Manager>> GetManager(string Id);
    }
}
