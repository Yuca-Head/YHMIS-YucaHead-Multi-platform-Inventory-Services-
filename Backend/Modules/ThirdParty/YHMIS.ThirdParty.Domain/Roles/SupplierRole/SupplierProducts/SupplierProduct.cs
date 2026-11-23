namespace YHMIS.Thirdparty.Domain.Roles.SupplierRole.SupplierProducts;

public readonly record struct SupplierProduct
{
    public SupplierProduct(SupplierProductCode id, decimal cost)
    {
        Id = id;
        Cost = cost is < 0 ? 0 : cost;
    }
    public readonly SupplierProductCode Id {get;} 
    public readonly decimal Cost {get;}
}