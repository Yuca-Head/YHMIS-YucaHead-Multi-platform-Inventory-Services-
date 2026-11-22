namespace YHMIS.Thirdparty.Domain.Roles;

public sealed partial record CustomerRole
{
    private CustomerRole(ThirdpartyRole role)
    {
        Role = role;
    }

    public ThirdpartyRole Role {get; private init;}
    

}