
using AutoMapper;
using HotelManagement.Core.Domains;
using System.Linq;
using HotelManagement.Core.DTOs.BookingDtos;
using HotelManagement.Core.DTOs.ReviewDTOs;
using HotelManagement.Core.DTOs;


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
            CreateMap<Hotel, GetHotelsDto>().ReverseMap();
            CreateMap<Rating,GetHotelByRatingsDto>().ReverseMap();
            CreateMap<Hotel,AddHotelDto>().ReverseMap();
            













            // Room Maps

            CreateMap<Room,AddRoomDto>().ReverseMap();

            CreateMap<Room, GetRoomDto>().ReverseMap();
            // RoomType Maps






            // Rating Maps


            // Gallery Maps


            //Customer
            CreateMap<Customer, GetCustomerDto>().ReverseMap();
                //.ForPath(dest => dest.AppUser.LastName, opt => opt.MapFrom(src => src.LastName))
                //.ForPath(dest => dest.AppUser.FirstName, opt => opt.MapFrom(src => src.FirstName))
                //.ForPath(dest => dest.AppUser.Gender, opt => opt.MapFrom(src => src.Gender))
                //.ForPath(dest => dest.AppUser.Age, opt => opt.MapFrom(src => src.Age))
                //.ForPath(dest => dest.AppUser.Avatar, opt => opt.MapFrom(src => src.Avatar))
                //.ForPath(dest => dest.AppUser.IsActive, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<Customer,AddCustomerAddressDto>().ReverseMap();


            //TransactionResponse Mapper
            CreateMap<Customer, GetCustomerDto>().ReverseMap();
            //Transaction Maps


            // aminity


            // reviewdto

            CreateMap<Review, AddReviewsDTO>().ReverseMap();
            CreateMap<GetReviewsDTO, GetReviewsDTO>().ReverseMap();

            //Review Maps
            CreateMap<Review, UpdateReviewDto>().ReverseMap();


            // IWshList Maps
            CreateMap<WishListDto, WishList>().ReverseMap()
            .ForPath(dest => dest.WishlistId, opt => opt.MapFrom(src => src.Id))
            .ForPath(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
            .ForPath(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForPath(dest => dest.FirstName, opt => opt.MapFrom(src => src.CustomerId))
            .ForPath(dest => dest.LastName, opt => opt.MapFrom(src => src.CustomerId))
            .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.HotelId))
            .ForPath(dest => dest.Address, opt => opt.MapFrom(src => src.HotelId))
            .ForPath(dest => dest.Price, opt => opt.MapFrom(src => src.Hotel.RoomTypes.Select(x => x.Id)))
            .ForPath(dest => dest.Discount, opt => opt.MapFrom(src => src.Hotel.RoomTypes.Select(x => x.Discount)));

            // Transaction Maps

            CreateMap<Payment, PaymentDTO>().ReverseMap();


            //Manager Maps



            //AppUser Maps


            //Manager Request Map
            CreateMap<ManagerRequest, ManagerRequestDTO>().ReverseMap();

        }
    }
}
