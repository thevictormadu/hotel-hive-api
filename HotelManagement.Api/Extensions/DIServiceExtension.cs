using FluentValidation;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Infrastructure.UnitOfWork;
using HotelManagement.Services.Services;

namespace HotelManagement.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add Service Injections Here
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add Repository Injections Here
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            // Add Model Services Injection Here


            // Add Fluent Validator Injections Here

        }
    }
}

