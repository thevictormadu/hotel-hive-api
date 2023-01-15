using HotelManagement.Core.DTOs;
using HotelManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.DTOs.ReviewDTOs;
using AutoMapper;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Core.Domains;
using HotelManagement.Core.IServices;
using System.Net;
using HotelManagement.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HotelManagement.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HotelDbContext _hotelDbContext;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper, HotelDbContext hotelDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hotelDbContext = hotelDbContext;
        }


        //public async Task<Response<AddReviewsDTO>> AddReviewAsync(AddReviewsDTO model, string customerId)
        //{
        //    var addReview = _mapper.Map<AddReviewsDTO>(model);
        //    _unitOfWork.reviewRepository.AddReview(customerId);
        //    return Response<AddReviewsDTO>.Success("Ok", addReview);

        //}

        //public async Task<Response<GetReviewsDTO>> GetHotelReviews(string hotelId)
        //{
        //    var getReviews = _unitOfWork.reviewRepository.GetHotelReviews(hotelId);
        //    var data = _mapper.Map<GetReviewsDTO>(getReviews);

        //    return Response<GetReviewsDTO>.Success("Ok", data);

        //}

        public async Task<Response<Review>> UpdateReview(string Id, UpdateReviewDto updateDto)
        {
            var updateReview = await _unitOfWork.reviewRepository.GetByIdAsync(x => x.Id == Id);
            var mappedUpdate = _mapper.Map<Review>(updateDto);

            if (updateReview == null)
            {
                return new Response<Review>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotel not found"
                };
            }
            else
            {
                _hotelDbContext.Update(updateReview);
                _unitOfWork.SaveChanges();
                return Response<Review>.Success("Updated Successfully", updateReview);
            }

        }

    }
}

