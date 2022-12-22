
using AutoMapper;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System.Linq;

namespace HotelManagement.Application.Utilities
{
    public class MapInitializer : Profile 
    {
        public Mapper regMapper { get; set; }
        public MapInitializer ()
        {
            // Authentication Maps
           var regConfig = new MapperConfiguration(conf => conf.CreateMap<RegisterDTO, AppUser>());
            regMapper = new Mapper(regConfig);

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


            //Review Maps


            // IWshList Maps

            // Transaction Maps


            //Manager Maps



            //AppUser Maps


            //Manager Request Map


        }
    }
}
