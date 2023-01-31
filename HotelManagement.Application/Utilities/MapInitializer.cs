
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
            CreateMap<GetCustomerDto, Customer>().ReverseMap()
                .ForPath(opt => opt.LastName, dest => dest.MapFrom(src => src.AppUser.LastName))
                .ForPath(opt => opt.FirstName, dest => dest.MapFrom(src => src.AppUser.FirstName))
                .ForPath(opt => opt.Gender, dest => dest.MapFrom(src => src.AppUser.Gender))
                .ForPath(opt => opt.Age, dest => dest.MapFrom(src => src.AppUser.Age))
                .ForPath(opt => opt.Avatar, dest => dest.MapFrom(src => src.AppUser.Avatar))
                .ForPath(opt => opt.IsActive, dest => dest.MapFrom(src => src.AppUser.IsActive));


            CreateMap<Customer,AddCustomerAddressDto>().ReverseMap();


            //TransactionResponse Mapper
            CreateMap<TransactionCustomerDto, Customer>().ReverseMap()
            .ForPath(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
            .ForPath(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
            .ForPath(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser.Gender))
            .ForPath(dest => dest.Age, opt => opt.MapFrom(src => src.AppUser.Age))
            .ForPath(dest => dest.Avatar, opt => opt.MapFrom(src => src.AppUser.Avatar)); ;
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
