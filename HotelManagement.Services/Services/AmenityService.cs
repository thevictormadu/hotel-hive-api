using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HotelManagement.Services.Services
{

    public class AmenityService : IAmenityService
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _hotelDbContext;
        protected DbSet<Amenity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;


        public AmenityService(IMapper mapper, HotelDbContext hotelDbContext, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _hotelDbContext = hotelDbContext;
            _dbSet = hotelDbContext.Set<Amenity>();
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        ///GetAmenities
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<AmenityDTO>>> GetAmenities()
        {
           var response = new Response<List<AmenityDTO>>();

            try
            {

                IEnumerable<Amenity> amenity = await _unitOfWork.AmenityRepository.GetAllAsync();
                var result = _mapper.Map<List<AmenityDTO>>(amenity);
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

        /// <summary>
        ///CreateAmenity
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>

        public async Task<Response<object>> CreateAmenity(CreateAmenitiesDTO createDto)
        {
            var response = new Response<object>();

            try
            {
                //    //Map amenity to CreateAmenitiesDTO
                Amenity amenity = _mapper.Map<Amenity>(createDto);
                await _unitOfWork.AmenityRepository.AddAsync(amenity);
                await _unitOfWork.AmenityRepository.SaveChangesAsync();
                var result = _mapper.Map<AmenityDTO>(amenity);
                response.Data = result;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Succeeded = true;
                response.Message = $"Successful";
                return response;

            }
            catch (Exception ex)
            {

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Succeeded = false;
                response.Message = $"Failed";
                response.Data = default;
                return response;
            }

        }



        public async Task<Response<Amenity>> UpdateAmenity(string id, UpdateAmenityDTO updateDto)
        {
            var response = new Response<Amenity>();
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    Response<UpdateAmenityDTO>.Fail("unsuccessful", 400);

                }
                Amenity model = _mapper.Map<Amenity>(updateDto);

                await _unitOfWork.AmenityRepository.UpdateAsync(model);
                await _unitOfWork.AmenityRepository.SaveChangesAsync();
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Succeeded = true;
                response.Message = $" {model.Name} was  Updated Successful";
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


        }//Getting a particular Amenity based on certain criteria
        public async Task<List<Amenity>> GetAllAsync(Expression<Func<Amenity, bool>>? filter = null)
        {
            IQueryable<Amenity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();

        }


        public async Task<Response<string>> DeleteAmenity(string id)
        {
            var response = new Response<string>();
            try
            {
                //check if the Id is null
                if (id == null)
                {
                    //if the id is null, then the Api response should return a badrequest
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    Response<string>.Fail("unsuccessful", 400);
                }
                var res = await _unitOfWork.AmenityRepository.GetByIdAsync(x => x.Id == id);
                //Check to see if the retrieved amenity to be deleted is null
                if (res == null)
                {
                    //if the retrieved amenity is null, then the response status code should be not Found
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    Response<string>.Fail("unsuccessful", 400);
                }
                //if all Checks are passed then the operation will now take place.
                await _unitOfWork.AmenityRepository.DeleteAsync(res);
                await _unitOfWork.AmenityRepository.SaveChangesAsync();
                response.Data = null;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Succeeded = true;
                response.Message = $"Amenity {res.Name} was Deleted successfull";
                return response;
            }
            catch (Exception)
            {
                //if all fails then the catch block will then Api response should return the responses below
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Succeeded = false;
                response.Message = $"Failed";
                response.Data = default;
                return response;
            }


        }
        public async Task<Amenity> GetByIdAsync(Expression<Func<Amenity, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<Amenity> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        //Getting a particular Amenity based on certain criteria

        public async Task<Amenity> GetAsync(Expression<Func<Amenity, bool>>? filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<Amenity> query = _dbSet;
            if (tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

      
    }
}
