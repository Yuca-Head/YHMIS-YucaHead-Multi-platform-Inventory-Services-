namespace YHMIS.Thirdparty.Domain.Roles.CustomerRole;

public sealed partial record CustomerRole
{
    private CustomerRole(ThirdpartyRole role)
    {
        Role = role;
    }

    public ThirdpartyRole Role {get; private init;}
    
}