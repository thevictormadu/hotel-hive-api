using FluentValidation;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.UnitOfWork;
using HotelManagement.Services.Services;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Core.IServices;
using HotelManagement.Core.Utilities;

namespace HotelManagement.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add Service Injections Here
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IManagerRequestService, ManagerRequestService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenDetails, TokenDetails>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IRoomRepository, RoomRespository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ILogger,Logger<TransactionService>>();



            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IHotelStatisticsService, HotelStatisticsService>();
            services.AddScoped<IWishlistService, WishlistService>();


            // Add Repository Injections Here
            services.AddScoped<IHotelRepository, HotelRepository>();

            services.AddScoped<IManagerRequestRepository, ManagerRequestRepository>();

            services.AddScoped<IWishlistRepository, WishlistRepository>();

            //services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();
        

            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();    
            // Add Model Services Injection Here
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Fluent Validator Injections Here

        }
    }
}

