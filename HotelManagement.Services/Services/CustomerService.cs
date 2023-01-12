using AutoMapper;
using HotelManagement.Application.Utility;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Core.Utilities;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _hotelDbContext;
        protected DbSet<Customer> _dbSet;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenDetails _tokenDetails;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IMapper mapper, HotelDbContext hotelDbContext, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, ITokenDetails tokenDetails,ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _hotelDbContext = hotelDbContext;
            _dbSet = hotelDbContext.Set<Customer>();
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tokenDetails = tokenDetails;
            _customerRepository = customerRepository;
        }

        public async Task<Response<List<GetCustomerDto>>> GetCustomers(int pageNo)
        {
            
                    var response = new Response<List<GetCustomerDto>>();

                try
                {
                    //IQueryable Item = await _context.Customers.Where();
                    //var gp = new GenericPagination<Datatype>();
                    //var paginatedItem = GenericPagination.ToPagedList(item,3,5)

                    //<Customer> customers = await _unitOfWork.customerRepository.GetAllAsync();
                    IQueryable customers = (IQueryable)await _unitOfWork.customerRepository.GetAllAsync();
                    //var gp = new GenericPagination<GetCustomerDto>();
                    var paginatedItem = GenericPagination<GetCustomerDto>.ToPagedList((IQueryable<GetCustomerDto>)customers, pageNo, 5);
                    var result = _mapper.Map<List<GetCustomerDto>>(customers);
                    response.Data = result;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Succeeded = true;
                    response.Message = $"Successful";
                    return response;
                }
                catch (Exception)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Succeeded = false;
                    response.Message = $"Failed";
                    response.Data = default;
                    return response;
                }
            
        }

        public async Task<Response<string>> AddCustomerAddress(AddCustomerAddressDto address)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(_tokenDetails.GetId());
                var customer = _mapper.Map<Customer>(address);
                customer.AppUser = user;
                customer.CreditCard = "xxxx-xxxx-xxxx-xxxx";
                customer.UpdatedAt = DateTime.Now;
                customer.CreatedAt = DateTime.Now;
                customer.Id = Guid.NewGuid().ToString();
                _hotelDbContext.Customers.Add(customer);
                _hotelDbContext.SaveChanges();
                //await _customerRepository.AddAsync(customer);
                //_unitOfWork.SaveChanges();
                return new Response<string>
                {
                    Message = "Address added",
                    StatusCode = (int)HttpStatusCode.Created,
                    Succeeded = true,
                };
            }
            catch
            {
                
                return new Response<string>
                {
                    Message = " Unable to add Address",
                    StatusCode = (int)HttpStatusCode.NotAcceptable,
                    Succeeded = false,
                };
            }
        }

        public async Task<Response<List<Customer>>> GetTopHotelCustomers(string hotelId)
        {
            var customer = await _customerRepository.GetTopHotelCustomers(hotelId);
            return customer;
        }
    }
}
