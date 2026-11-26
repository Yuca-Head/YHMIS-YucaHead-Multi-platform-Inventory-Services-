using Microsoft.Extensions.DependencyInjection;
using YHMIS.Identity.Application.Authentication.Login;

namespace YHMIS.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityApplication(
        this IServiceCollection services)
    {
        services.AddScoped<LoginHandler>();

        return services;
    }
}