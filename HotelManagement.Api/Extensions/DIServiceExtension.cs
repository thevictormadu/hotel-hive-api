using FluentValidation;
using HotelManagement.Core.IServices;
using HotelManagement.Services.Services;

namespace HotelManagement.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add Service Injections Here
            services.AddSingleton<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IEmailService, EmailService>();

            // Add Repository Injections Here

            // Add Model Services Injection Here


            // Add Fluent Validator Injections Here
            
        }
    }
}

