namespace YHMIS.Identity.Application.Authentication.Dtos;

public readonly record struct UserDto(string Email, string UserName, DateTimeOffset CreateAt);