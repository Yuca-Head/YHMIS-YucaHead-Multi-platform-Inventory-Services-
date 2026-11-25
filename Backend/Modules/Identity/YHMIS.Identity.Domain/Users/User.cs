namespace YHMIS.Identity.Domain.Users;

public readonly record struct User(int Id, string Email, string Password, string UserName, bool IsActive);