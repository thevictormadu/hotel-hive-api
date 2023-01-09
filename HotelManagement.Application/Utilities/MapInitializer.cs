
using AutoMapper;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs.ReviewDTOs;
using System.Linq;

namespace HotelManagement.Application.Utilities
{
    public class MapInitializer : Profile
    {
        public MapInitializer ()
        {
            // Authentication Maps


            // Amenity Maps


            // Booking Maps


            // Hotel Maps



            // Room Maps


            // RoomType Maps






            // Rating Maps


            // Gallery Maps


            //Customer



            //TransactionResponse Mapper

            //Transaction Maps



            // aminity


            // reviewdto
            CreateMap<Review, AddReviewsDTO>().ReverseMap();
            CreateMap<GetReviewsDTO, GetReviewsDTO>().ReverseMap();

          

            //Review Maps
            

            // IWshList Maps
           
            // Transaction Maps
           

            //Manager Maps
            


            //AppUser Maps
           

            //Manager Request Map

           
        }
    }
}
