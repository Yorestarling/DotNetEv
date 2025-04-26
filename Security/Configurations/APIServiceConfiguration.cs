using Microsoft.Extensions.DependencyInjection;
using Security.Jwt;

namespace Security.Configurations;

public static class APIServiceConfiguration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtToken, JwtToken>();
        
    }
}
