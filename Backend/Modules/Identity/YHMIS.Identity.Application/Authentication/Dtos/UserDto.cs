namespace YHMIS.Identity.Application.Authentication.Dtos;

public sealed record UserDto(int UserId, string Email, string UserName, string[] Roles);