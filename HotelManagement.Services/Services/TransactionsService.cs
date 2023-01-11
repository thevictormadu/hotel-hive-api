using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using AutoMapper;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Application.Utility;
using Microsoft.Extensions.Configuration;
//
namespace HotelManagement.Services.Services
{
    public class TransactionsServices : ITransactionsServices

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ILogger _logger;

        public TransactionsServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_logger = logger;
        }

        public async Task<Response<List<PaymentDTO>>> GetAllUserTransactionForAnHotel(string customerID, string hotelId, int pageNumber, int pageSize)
        {
            try
            {
                var bookings = await _unitOfWork.transactionsRepository.GetAllAsync(booking => booking.CustomerId == customerID && booking.HotelId == hotelId);

                //_logger.LogInformation("Customer repository Triggered); 
                if (bookings == null)
                {
                    return Response<List<PaymentDTO>>.Fail($"Customer does not exist in this hotel's register");
                }
                var transactions = bookings.Select(p => p.Payment).AsQueryable();

                if (transactions == null)
                {
                    return Response<List<PaymentDTO>>.Fail($"No transactions found for this customer");
                }

                var paginatedTran = GenericPagination<Payment>.ToPagedList(transactions, pageNumber, pageSize);
                var data = _mapper.Map<List<PaymentDTO>>(transactions);
                return Response<List<PaymentDTO>>.Success("Successful", data);
            }

            catch (Exception ex)
            {
                //_logger.LogError("Unable to retrieve users transactions for hotel");
                return Response<List<PaymentDTO>>.Fail($"");
            }

        }


    }
}