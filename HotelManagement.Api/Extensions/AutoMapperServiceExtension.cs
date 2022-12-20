using HotelManagement.Application.Utilities;
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
