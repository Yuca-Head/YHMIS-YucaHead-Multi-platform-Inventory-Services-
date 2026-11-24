namespace YHMIS.Thirdparty.Domain.Roles.SupplierRole;

public sealed partial record SupplierRole
{
    private SupplierRole(ThirdpartyRole role)
    {
        Role = role;
    }

    public ThirdpartyRole Role {get; private init;}

}