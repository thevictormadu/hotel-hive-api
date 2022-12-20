using FluentValidation;
using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Reflection;
using System.Text;
using HotelManagement.Api.Extensions;
using Serilog;
using HotelManagement.Api.Policies;
using FluentValidation.AspNetCore;
using HotelManagement.Infrastructure.Seeding;

namespace HotelManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            var services = builder.Services;

            // Add services to the container.
            builder.Services.AddHttpClient();
            //builder.Services.AddDbContextAndConfigurations(builder.Environment, config);

            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddScoped<IUrlHelper>(x =>
                    x.GetRequiredService<IUrlHelperFactory>()
                        .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));

            //For Entity Framework

            builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer
            (builder.Configuration.GetConnectionString("ConnStr")));

            //builder.Services.AddControllers();
            // Configure Mailing Service
            builder.Services.ConfigureMailService(config);


            builder.Services.AddSingleton(Log.Logger);

            // Adds our Authorization Policies to the Dependecy Injection Container
            builder.Services.AddPolicyAuthorization();

            // Configure Identity
            builder.Services.ConfigureIdentity(); 

            builder.Services.AddAuthentication();

            // Add Jwt Authentication and Authorization
            services.ConfigureAuthentication(config);

            // Configure AutoMapper
            services.ConfigureAutoMappers();

            // Configure Cloudinary

            //builder.Services.AddCloudinary(CloudinaryServiceExtension.GetAccount(Configuration));

            builder.Services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling
            = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            builder.Services.AddControllers()
                .AddNewtonsoftJson(op => op.SerializerSettings
                    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddMvc().AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssemblyContaining<Program>();
                fv.ImplicitlyValidateChildProperties = true;
            });

            builder.Services.AddSwagger();

            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            // Register Dependency Injection Service Extension
            builder.Services.AddDependencyInjection();

            //For Entity Framework

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            Seeder.SeedData(app).Wait();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run(); 
        }
    }
}