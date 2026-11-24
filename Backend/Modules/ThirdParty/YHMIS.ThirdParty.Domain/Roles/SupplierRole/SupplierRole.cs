using YHMIS.Shared.Abstractions;

namespace YHMIS.Thirdparty.Domain.Roles.SupplierRole;

public sealed partial record SupplierRole : IEntity<string>
{
    private SupplierRole(ThirdpartyRole role)
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