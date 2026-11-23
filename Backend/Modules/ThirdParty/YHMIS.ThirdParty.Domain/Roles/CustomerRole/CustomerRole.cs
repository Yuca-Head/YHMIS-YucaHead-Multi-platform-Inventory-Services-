using YHMIS.Shared.Abstractions;

namespace YHMIS.Thirdparty.Domain.Roles.CustomerRole;

public sealed partial record CustomerRole : IEntity<string>
{
    private CustomerRole(ThirdpartyRole role)
    {
        Role = role;
        Key = Role.Key;
    }

    public ThirdpartyRole Role {get; private init;}


    /// <summary>
    /// Role-Code.
    /// </summary>
    public string Key {get;}
}