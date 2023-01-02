
using AutoMapper;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
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
           CreateMap<Rating,GetHotelByRatingsDto>().ReverseMap();


            // Room Maps

            CreateMap<Room, GetRoomDto>().ReverseMap();
            // RoomType Maps
            





            // Rating Maps
            

            // Gallery Maps
            

            //Customer
           


            //TransactionResponse Mapper

            //Transaction Maps
            


            // aminity
            

            // reviewdto
          

            //Review Maps
            

            // IWshList Maps
           
            // Transaction Maps
           

            //Manager Maps
            


            //AppUser Maps
           

            //Manager Request Map

           
        }
    }
}
