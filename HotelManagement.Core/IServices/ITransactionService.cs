
﻿using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
﻿using HotelManagement.Core.Domains;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface ITransactionService
    {
        //ITransaction for display all transaction for admin
        Task<Response<IEnumerable<PaymentDTO>>> DisplayAllTransactionToAdmin();

       Task<Response<List<RoomTransactionDTO>>>GetRoomTransactionsByManger(string managerId);
        Task<Response<List<RoomTransactionDTO>>> GetAllRoomsTransactions(string hotelId);




    }
}
