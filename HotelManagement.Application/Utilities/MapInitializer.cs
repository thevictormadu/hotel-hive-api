
using AutoMapper;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System.Linq;
using HotelManagement.Core.DTOs.BookingDtos;

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
            CreateMap<Booking, BookingRequestDto>().ReverseMap()
                .ForPath(dest => dest.RoomType.Name, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForPath(dest => dest.RoomType.Hotel.Name, opt => opt.MapFrom(src => src.HotelId))
                .ForPath(dest => dest.Customer.AppUser.LastName, opt => opt.MapFrom(src => src.CustomerId))
                .ForPath(dest => dest.Customer.AppUser.FirstName, opt => opt.MapFrom(src => src.CustomerId))
                //.ForMember(x => x.Customer.AppUser.LastName, y => y.MapFrom(src => src.CustomerName));
                .ForPath(dest => dest.RoomType.Name, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForPath(dest => dest.RoomType.Hotel.Name, opt => opt.MapFrom(src => src.HotelId))
                .ForPath(dest => dest.Customer.AppUser.LastName, opt => opt.MapFrom(src => src.CustomerId))
                .ForPath(dest => dest.Customer.AppUser.FirstName, opt => opt.MapFrom(src => src.CustomerId));
                 //.ForMember(x => x.Customer.AppUser.LastName, y => y.MapFrom(src => src.CustomerName));
            CreateMap<Booking, BookingResponseDto>().ReverseMap();

            // Hotel Maps
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();
            CreateMap<Hotel, GetHotelByStateDto>().ReverseMap();
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
