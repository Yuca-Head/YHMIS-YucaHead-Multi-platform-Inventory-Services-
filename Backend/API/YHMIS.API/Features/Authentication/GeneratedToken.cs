namespace YHMIS.API.Features.Authentication;

public sealed record GeneratedToken(
    string AccessToken,
    DateTimeOffset ExpiresAt);