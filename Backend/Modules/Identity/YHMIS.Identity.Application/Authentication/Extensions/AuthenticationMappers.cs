using YHMIS.Identity.Application.Authentication.Dtos;

namespace YHMIS.Identity.Application.Authentication.Extensions;


public static class AuthenticationMappers
{
    public static UserDto UserToDto(this User user, IEnumerable<string> roles)
    => new(user.Id, user.Email! ?? "", user.UserName! ?? "", [.. roles]);
}