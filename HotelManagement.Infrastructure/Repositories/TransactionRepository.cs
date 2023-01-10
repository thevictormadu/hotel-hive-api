using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Payment>, ITransactionRepository
    {
        protected readonly HotelDbContext _db;
        public TransactionRepository(HotelDbContext db) : base(db)
        {
            //_looger
            _db = db;
           
        }


    }
}
