using YHMIS.Shared.Abstractions;

namespace YHMIS.Thirdparty.Domain.Roles.SupplierRole.SupplierProducts;

public readonly record struct SupplierProduct : IEntity<SupplierProductCode>
{
    public SupplierProduct(SupplierProductCode id, decimal cost)
    {
        Key = id;
        Cost = cost is < 0 ? 0 : cost;
    }
    public readonly SupplierProductCode Key {get;} 
    public readonly decimal Cost {get;}
}