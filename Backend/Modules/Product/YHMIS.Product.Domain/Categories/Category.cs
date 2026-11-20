using YHMIS.Shared.Results;

namespace YHMIS.Product.Domain.Categories;

public sealed partial record class Category
{
    private Category(int id, string name)
    {
        Id = id;
        Name = name;
    }

    private Category() { }

    public int Id { get; private init; } = -1;

    public string Name
    {
        get;
        private init => field = value.Trim();
    } = "";
}
