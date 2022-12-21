using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class TransactionReferenceDTO
    {
        public string TransactionReference { get; set; }
        public decimal Amount { get; set; }
        public string MethodOfPayment { get; set; }
        public Booking Booking { get; set; }


    }
}
