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

        public async Task<Response<IEnumerable<GetCustomerDto>>> GetCustomers(int pageNo)
        {
            var response = new Response<IEnumerable<GetCustomerDto>>();
            try
            {
                //var customers = await _unitOfWork.customerRepository.GetAllAsync();
                var customers = await _customerRepository.GetCustomers(pageNo);
                var result = _mapper.Map<IEnumerable<GetCustomerDto>>(customers);
                var paginatedItem = result.Skip((pageNo - 1) * 5).Take(5);
                response.Data = paginatedItem;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Succeeded = true;
                response.Message = $"Successful";
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Succeeded = false;
                response.Message = $"Failed: {ex.Message}";
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

        public async Task<Response<List<GetCustomersByHotelDto>>> GetCustomersByHotelId(string hotelId)
        {
            var customers = await _unitOfWork.customerRepository.GetCustomersByHotel(hotelId);
            if (customers.Count == 0)
            {
                return Response<List<GetCustomersByHotelDto>>.Fail("Customers not found");
            }
            //Create a list to store the GetCustomerByHotelDTO
            var hotelCustomers = new List<GetCustomersByHotelDto>();

            //loop through each customer associated with the a hotel
            foreach (var customer in customers)
            {
                ////find the booking for the current hotel
                //var booking = customers.Select(x => x.Bookings);

                //create a new 
                var hotelCustDtos = new GetCustomersByHotelDto
                {
                    CustomerFirstName = customer.AppUser.FirstName,
                    CustomerLastName = customer.AppUser.LastName,
                    EmailAddress = customer.AppUser.Email,
                    State = customer.State,
                    Gender = customer.AppUser.Gender,
                };
                hotelCustomers.Add(hotelCustDtos);
            }
            return Response<List<GetCustomersByHotelDto>>.Success("Successful", hotelCustomers, 200);
        }

        public async Task<Response<Customer>> GetCustomer(string Id)
        {
            var result = await _unitOfWork.customerRepository.GetCustomer(Id);

            return Response<Customer>.Success("Successfull", result, 200);
        }

    }
}
