using FluentValidation;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.UnitOfWork;

namespace HotelManagement.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add Service Injections Here


            // Add Repository Injections Here
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Add Model Services Injection Here


            // Add Fluent Validator Injections Here
            
        }
    }
}

