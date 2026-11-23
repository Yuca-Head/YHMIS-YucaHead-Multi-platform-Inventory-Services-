using YHMIS.Shared.Abstractions;
using YHMIS.Shared.Results;

namespace YHMIS.Product.Domain.UnitsOfMeasure;

public sealed partial record UnitOfMeasure : IEntity<int>
{
    private UnitOfMeasure(int id, string name)
    {
        Key = id;
        Name = name;
    }

    private UnitOfMeasure() { }

    public int Key { get; private init; } = -1;

    public string Name
    {
        get;
        private init => field = value.Trim();
    } = "";
}