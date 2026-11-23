using YHMIS.Shared.Abstractions;
using YHMIS.Shared.Results;

namespace YHMIS.Product.Domain.Categories;

public sealed partial record Category : IEntity<int>
{
    private Category(int id, string name)
    {
        Key = id;
        Name = name;
    }

    private Category() { }

    public int Key { get; private init; } = -1;

    public string Name
    {
        get;
        private init => field = value.Trim();
    } = "";
}
