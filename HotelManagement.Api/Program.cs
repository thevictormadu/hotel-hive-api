using FluentValidation.AspNetCore;
using HotelManagement.Api.Extensions;
using HotelManagement.Api.Policies;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Infrastructure.Seeding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
            //builder.Services.AddScoped<IHotelServices, HotelRepository>();

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
            builder.Services.AddCloudinary(CloudinaryServiceExtension.GetAccount(config));

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

            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelManagementAPI", Version = "v1" });
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
            //          Enter 'Bearer' [space] and then your token in the text input below.
            //          \r\n\r\nExample: 'Bearer 12345abcdef'",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer"
            //    });

            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //          {
            //            {
            //              new OpenApiSecurityScheme
            //              {
            //                Reference = new OpenApiReference
            //                  {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                  },
            //                  Scheme = "oauth2",
            //                  Name = "Bearer",
            //                  In = ParameterLocation.Header,

            //                },
            //                new List<string>()
            //              }
            //            });
            //});

            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            // Register Dependency Injection Service Extension
            builder.Services.AddDependencyInjection();

            //For Entity Framework

            var app = builder.Build();

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Seeder.SeedData(app).Wait();
             
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}



