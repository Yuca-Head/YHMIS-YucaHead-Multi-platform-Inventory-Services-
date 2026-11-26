namespace YHMIS.Identity.Application.Authentication.Commands;

public readonly record struct AuthCommand(string Email, string Password);  