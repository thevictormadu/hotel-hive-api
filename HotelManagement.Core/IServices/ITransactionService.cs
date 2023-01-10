using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface ITransactionService
    {
        Task<Response<IEnumerable<PaymentDTO>>> DisplayAllTransactionToAdmin();
    }
}
