using YHMIS.Thirdparty.Domain.Roles;

namespace YHMIS.Thirdparty.Domain.Thirdparties;

public sealed partial record ThirdParty
{
    private ThirdParty(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id {get; private init;}
    public string Name {get; private init;}
}