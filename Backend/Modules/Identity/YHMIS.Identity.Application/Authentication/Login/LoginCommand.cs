namespace YHMIS.Identity.Application.Authentication.Login;

public readonly record struct LoginCommand(string Email, string Password);  