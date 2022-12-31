using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HotelDbContext context;
        private IAmenityRepository _amenityRepository;
        public UnitOfWork(HotelDbContext context)
        {
            this.context = context;
        }
        //create an instance, if it has not been created before

        public IAmenityRepository AmenityRepository 
        {
            get
            {
                if(_amenityRepository == null)
                {
                    //lazy initialize the Amenity if requested
                    _amenityRepository = new AmenityRepository(context);
    }
                return _amenityRepository;
            }
        }

        //=>
        // _amenityRepository ??= new AmenityRepository(context);
        public async Task CompleteAsync()
        {
            context.SaveChangesAsync();
        }
        public void Dispose()
        {
            
        }
    }
}
