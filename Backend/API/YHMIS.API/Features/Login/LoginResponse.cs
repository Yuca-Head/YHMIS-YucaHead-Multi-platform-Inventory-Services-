using YHMIS.Identity.Application;

namespace YHMIS.API.Features.Login;

public sealed record LoginResponse(string AccessToken, DateTimeOffset ExpiresAt, User User);