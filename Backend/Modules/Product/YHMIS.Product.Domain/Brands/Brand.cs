using YHMIS.Shared.Abstractions;

namespace YHMIS.Product.Domain.Brands;

public sealed partial record Brand : IEntity<int>
{
    private Brand(int id, string name)
    {
        Key = id;
        Name = name;
    }
    private Brand() { }

    public int Key { get; private init; } = -1;
    public string Name { get; private init => field = value.Trim(); } = "";
}
