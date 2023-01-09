
using AutoMapper;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System.Linq;
using HotelManagement.Core.DTOs.ReviewDTOs;

namespace HotelManagement.Application.Utilities
{
    public class MapInitializer : Profile
    {
        public Mapper regMapper { get; set; }
        public MapInitializer()
        {
            // Authentication Maps

            var regConfig = new MapperConfiguration(conf => conf.CreateMap<RegisterDTO, AppUser>());
            regMapper = new Mapper(regConfig);
            // Amenity Maps

            CreateMap<Amenity, AmenityDTO>().ReverseMap();
            CreateMap<Amenity, CreateAmenitiesDTO>().ReverseMap();
            CreateMap<Amenity, UpdateAmenityDTO>().ReverseMap();

            // Booking Maps


            // Hotel Maps
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();
           // CreateMap<Hotel, GetHotelsDto>().ReverseMap();
           CreateMap<Rating,GetHotelByRatingsDto>().ReverseMap();
            CreateMap<Hotel,AddHotelDto>().ReverseMap();    



            // Room Maps
            CreateMap<Room,AddRoomDto>().ReverseMap();


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
