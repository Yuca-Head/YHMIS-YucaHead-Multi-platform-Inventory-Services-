namespace YHMIS.Identity.Application.Authentication.Results;

public sealed record LoginResult(int UserId, string Email, string UserName, string[] Roles);