using Microsoft.AspNetCore.Identity;

namespace YHMIS.Identity.Application;

public sealed class User: IdentityUser<int>
{
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}