using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;
using YHMIS.Identity.Application.Authentication.Results;

namespace YHMIS.Identity.Application.Authentication.Login;

public sealed class LoginHandler(
    UserManager<User> userManager)
{
    public async Task<OneOf<Success<User>, 
    Unauthorized, 
    Shared.Results.Error>> 
    Handle(LoginCommand request,
    CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(
            request.Email);

        if (user is null || !user.IsActive)
            return new Unauthorized();

        var valid = await userManager.CheckPasswordAsync(user,
        request.Password);  

        if (!valid)
            return new Unauthorized();

        return new Success<User>(user);
    }
}