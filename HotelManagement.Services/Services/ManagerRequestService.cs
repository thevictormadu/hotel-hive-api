using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Ocsp;
using HotelManagement.Core.Utilities;
using System.IdentityModel.Tokens.Jwt;

namespace HotelManagement.Services.Services
{
    public class ManagerRequestService : IManagerRequestService
    {
        private readonly IManagerRequestRepository _managerRequestRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly ITokenDetails _tokenDetails;

        public ManagerRequestService(IManagerRequestRepository managerRequestRepository, 
            IUnitOfWork unitOfWork, IMapper mapper,IEmailService emailService, 
            ITokenService tokenService, ITokenDetails tokenDetails)
        {
            _managerRequestRepository = managerRequestRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _tokenService = tokenService;
            _tokenDetails = tokenDetails;
        }

        public async Task<Response<string>> ManagerRequest(ManagerRequestDTO managerRequest)
        {
            var checkManager = await _managerRequestRepository.GetAllAsync();
            if(checkManager.Any(man => man.Email == managerRequest.Email))
            {
                return new Response<string>
                {
                    Data = null,
                    Message = "This user manager already request to join the service, please check you mail for invitation link",
                    Succeeded = false,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            var managerMap = _mapper.Map<ManagerRequest>(managerRequest);
            managerMap.UpdatedAt = DateTime.Now;
            managerMap.Id = Guid.NewGuid().ToString();
            managerMap.Token = _tokenService.CreateToken(managerMap);
            await _managerRequestRepository.AddAsync(managerMap);
            _unitOfWork.SaveChanges();
            return new Response<string>
            {
                Data = "Our team will review you application and get back to you via mail between 25 working days",
                Message = "Request sent to the admin",
                Succeeded = true,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        public async Task<Response<string>> AdminSendInvite(string requestId)
        {
            //var manager = await _managerRequestRepository.GetManagerRequestById(requestId);
            var manager = await _managerRequestRepository.GetByIdAsync((item) => item.Id == requestId);
            if(manager == null)
            {
                return new Response<string>
                {
                    Message = "This Manager request does not exist",
                    Succeeded = false,
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            manager.Token = _tokenService.CreateToken(manager);
            manager.ExpiresAt = DateTime.Now.AddDays(5);
            //await _managerRequestRepository.UpdateAsync(manager,manager);
            var subject = "Manager request Approval for Hotel listing platform";
            //https://localhost:7255/api/Authentication/Login
            var content = $"Click on this link to register as a manager <a href='https://localhost:7255/api/ManagerRequest/ManagerAcceptInvite/{manager.Token}' target='_blank'>Register</a> " +
                $"You can also copy and paste to another tab: https://localhost:7255/api/ManagerRequest/ManagerAcceptInvite/{manager.Token}" +
                $"\n\r Link expires in 5-days";
            var message = new EmailMessage(new List<string> { manager.Email }, subject, content);
            await _emailService.SendEmailAsync(message);
            _unitOfWork.SaveChanges();
            return new Response<string>
            {
                Message = "Manager has been confirmed and a mail has been sent for registration.",
                Succeeded = true,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<Response<string>> ManagerAcceptInvite(string token)
        {

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var x = jwt.Claims;
            string user = jwt.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var manager = await _managerRequestRepository.GetManagerRequestById(user);
            if (manager == null)
            {
                return new Response<string>
                {
                    Message = "This link is invalid, please try to reapply for management request",
                    Succeeded = false,
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            if (!manager.ConfirmationFlag) manager.ConfirmationFlag = true;
            //await _managerRequestRepository.UpdateAsync(manager, manager);
            _unitOfWork.SaveChanges();

            return new Response<string>
            {
                Message="Your request as a manager has been approved",
                Succeeded=true,
                StatusCode=(int)HttpStatusCode.Accepted
            };
        }


        public async Task<Response<Manager>> GetManager(string Id)
        {
            //var result = await _unitOfWork.customerRepository.GetByIdAsync(x => x.Id == Id);
            var result = await _unitOfWork.managerRepository.GetManager(Id);

            return Response<Manager>.Success("Successfull", result, 200);
        }

    }
}
