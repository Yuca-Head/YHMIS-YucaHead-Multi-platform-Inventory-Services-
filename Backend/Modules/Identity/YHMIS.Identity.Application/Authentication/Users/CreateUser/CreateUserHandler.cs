using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;
using YHMIS.Identity.Application.Authentication.Commands;
using YHMIS.Identity.Application.Authentication.Results;
namespace YHMIS.Identity.Application.Authentication.Users.CreateUser;

public sealed class CreateUserHandler(UserManager<User> userManager)
{
    public async Task<OneOf<Success, 
    EmailAlreadyExists, 
    Error<IEnumerable<Shared.Results.Error>>>> 
    Handle(AuthCommand request)
    {
        var existingUser = await userManager.FindByEmailAsync(
            request.Email);

        if (existingUser is not null)
            return new EmailAlreadyExists();

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            CreatedAt = DateTimeOffset.UtcNow,
            IsActive = true
        };

        var result = await userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
            return new Error<IEnumerable<Shared.Results.Error>>
            (result.Errors.Select(e => new Shared.Results.Error(e.Code, e.Description)));

        return new Success();
    }
}