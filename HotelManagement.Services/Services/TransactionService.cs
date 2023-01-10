using AutoMapper;
using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;

namespace HotelManagement.Services.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;


        }
        public async Task<Response<IEnumerable<PaymentDTO>>> DisplayAllTransactionToAdmin()
        {
            var response = await _unitOfWork.Payment.GetAllAsync();

            var res = _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentDTO>>(response);
            try
            {
                if (res == null)
                    return Response<IEnumerable<PaymentDTO>>.Fail($"no request found {res}", 404);

                return Response<IEnumerable<PaymentDTO>>.Success("successful", res, 200);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Response<IEnumerable<PaymentDTO>>.Fail($"no request found {res}", 404);
            }
        }
    }
}
