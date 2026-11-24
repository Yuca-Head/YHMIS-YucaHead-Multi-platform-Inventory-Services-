using YHMIS.Product.Domain.Brands;
using YHMIS.Product.Domain.Categories;
using YHMIS.Product.Domain.UnitsOfMeasure;

namespace YHMIS.Product.Domain.Products;

public partial record class Product
{
    public const int SkuLength = 8;
    private Product(int id, string name, string sku, UnitOfMeasure unitOfMeasure, Category category,
    Brand brand, string? description, bool isActive = true)
    {
        Id = id;
        Name = name;
        SKU = sku;
        UnitOfMeasure = unitOfMeasure;
        Category = category;
        Brand = brand;
        Description = description;
        IsActive = isActive;
    }

    public int Id {get; private init;} = -1;

    /// <summary>
    /// Can change Creation out of this class for creation purposes.
    /// </summary>
    public DateTimeOffset CreatedAt {get; init;}
    public UnitOfMeasure UnitOfMeasure {get; private init;}
    public Category Category {get; private init;}
    public Brand Brand {get; private init;}
    public string Name {get; private init => field = value.Trim();} = "";
    public string SKU {get; private init => field = value.Trim();} = "";
    public string? Description {get; private init => field = value?.Trim();} = "";
    public bool IsActive {get; private init;} = true;

}