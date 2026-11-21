using YHMIS.Shared.Results;

namespace YHMIS.Product.Domain.UnitsOfMeasure;

public sealed partial record UnitOfMeasure
{
    private UnitOfMeasure(int id, string name)
    {
        Id = id;
        Name = name;
    }

    private UnitOfMeasure() { }

    public int Id { get; private init; } = -1;

    public string Name
    {
        get;
        private init => field = value.Trim();
    } = "";
}