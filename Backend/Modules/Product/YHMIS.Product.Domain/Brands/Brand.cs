namespace YHMIS.Product.Domain.Brands;

public sealed partial record Brand
{
    private Brand(int id, string name)
    {
        Id = id;
        Name = name;
    }
    private Brand() { }

    public int Id { get; private init; } = -1;
    public string Name { get; private init => field = value.Trim(); } = "";
}
