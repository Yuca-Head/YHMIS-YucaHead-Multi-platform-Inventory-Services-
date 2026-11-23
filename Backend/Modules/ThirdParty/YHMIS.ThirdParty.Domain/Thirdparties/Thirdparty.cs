using YHMIS.Shared.Abstractions;

namespace YHMIS.Thirdparty.Domain.Thirdparties;

public sealed partial record ThirdParty : IEntity<int>
{
    private ThirdParty(int id, string name)
    {
        Key = id;
        Name = name;
    }

    public int Key {get; private init;}
    public string Name {get; private init;}
}