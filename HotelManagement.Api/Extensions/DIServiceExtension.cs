using FluentValidation;
using HotelManagement.Core.IServices;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.UnitOfWork;
using HotelManagement.Services.Services;
using HotelManagement.Infrastructure.Repositories;

namespace HotelManagement.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add Service Injections Here
             services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<IHotelService, HotelService>();

            // Add Repository Injections Here
            //services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IAmenityService, AmenityService>();
            // Add Model Services Injection Here
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Fluent Validator Injections Here

        }
    }
}

