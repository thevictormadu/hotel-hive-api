using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class TransactionReferenceService : ITransactionReferenceService
    {
        public List<TransactionReferenceDTO> Transactions { get; set; } = new List<TransactionReferenceDTO>();     



        public async Task <TransactionReferenceDTO> GetAllRoomTransaction (TransactionReferenceDTO transactionReferenceDTO)
        {
            Transactions.Add(transactionReferenceDTO);
            return transactionReferenceDTO;
            
            //if (transactionReferenceDTO == null) { }
        }

        public Task GetAllRoomTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
