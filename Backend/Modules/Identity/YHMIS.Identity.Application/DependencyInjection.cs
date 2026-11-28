using Microsoft.Extensions.DependencyInjection;
using YHMIS.Identity.Application.Authentication.Login;
using YHMIS.Identity.Application.Authentication.Users.CreateUser;
using YHMIS.Identity.Application.Authentication.Users.GetUser;

namespace YHMIS.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityApplication(
        this IServiceCollection services)
    {
        services.AddScoped<LoginHandler>();
        services.AddScoped<CreateUserHandler>();
        services.AddScoped<GetUserHandler>();

        return services;
    }
}