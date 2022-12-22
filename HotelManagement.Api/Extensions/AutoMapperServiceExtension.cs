using HotelManagement.Application.Utilities;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Api.Extensions
{
    public static class AutoMapperServiceExtension
    {
        public static void ConfigureAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapInitializer));
        }
    }
}
