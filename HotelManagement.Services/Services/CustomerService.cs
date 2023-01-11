using AutoMapper;
using HotelManagement.Application.Utility;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
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

        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IMapper mapper, HotelDbContext hotelDbContext, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _hotelDbContext = hotelDbContext;
            _dbSet = hotelDbContext.Set<Customer>();
            _unitOfWork = unitOfWork;
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

        
    }
}


