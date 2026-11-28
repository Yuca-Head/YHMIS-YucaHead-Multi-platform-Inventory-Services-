using YHMIS.Identity.Application;

namespace YHMIS.API.Features.Authentication.Responses;

public sealed record AuthResponse(string AccessToken, DateTimeOffset ExpiresAt, User User);