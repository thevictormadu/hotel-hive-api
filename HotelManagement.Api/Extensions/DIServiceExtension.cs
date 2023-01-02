using FluentValidation;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Core.Utilities;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Services.Services;

namespace HotelManagement.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add Service Injections Here

            // Add Repository Injections Here
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenDetails, TokenDetails>();
            // Add Model Services Injection Here


            // Add Fluent Validator Injections Here

        }
    }
}

