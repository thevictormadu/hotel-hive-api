using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace HotelManagement.Api.Extensions
{
    public static class AuthenticationServiceExtension
    {
        /// <summary>
        /// Configures JWT Authentication and Sets up Policy Based Authorization Services.
        /// Returns Services of type IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = configuration["JwtSettings:ValidAudience"],
                ValidIssuer = configuration["JwtSettings:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding
                    .UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.TokenValidationParameters = tokenValidationParameters;

           });

        }
    }
}
