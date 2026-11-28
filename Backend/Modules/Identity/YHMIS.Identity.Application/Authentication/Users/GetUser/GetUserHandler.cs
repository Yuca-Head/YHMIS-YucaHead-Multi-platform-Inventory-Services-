using Microsoft.AspNetCore.Identity;
using YHMIS.Identity.Application.Authentication.Dtos;
using YHMIS.Identity.Application.Authentication.Extensions;
using YHMIS.Identity.Application.Authentication.Results;

namespace YHMIS.Identity.Application.Authentication.Users.GetUser;

public sealed class GetUserHandler(UserManager<User> userManager)
{
    public async Task<UserDto?> GetUserAsync(int id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());


        if(user is null)
            return null;

        return user.UserToDto(await userManager.GetRolesAsync(user));
    }
}