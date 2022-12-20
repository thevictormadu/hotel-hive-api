using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Api.Policies
{
    public static class AuthorizationMiddleWare
    {
        public static void AddPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(configure =>
            {
                
            });
        }
    }
}
