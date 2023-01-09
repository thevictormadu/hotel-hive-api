using FluentValidation;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.UnitOfWork;
using HotelManagement.Services.Services;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Core.IServices;

namespace HotelManagement.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add Service Injections Here
             services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();

            // Add Repository Injections Here
            services.AddScoped<IHotelRepository, HotelRepository>();
            //services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();

            // Add Model Services Injection Here
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Fluent Validator Injections Here

        }
    }
}

