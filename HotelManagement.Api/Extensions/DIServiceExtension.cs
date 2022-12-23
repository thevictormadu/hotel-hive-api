using FluentValidation;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.Utilities;
using HotelManagement.Infrastructure.Repositories;

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
            services.AddScoped<IToken, Token>();
            services.AddScoped<ITokenDetails, TokenDetails>();
            // Add Model Services Injection Here


            // Add Fluent Validator Injections Here

        }
    }
}

