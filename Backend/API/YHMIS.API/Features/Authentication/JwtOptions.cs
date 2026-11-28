namespace YHMIS.API.Features.Authentication;


public sealed class JwtOptions
{
    public const string Section = "Jwt";

    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; } = "";
    public int ExpirationMinutes { get; init; } = 30;
}