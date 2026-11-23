using YHMIS.Shared.Abstractions;

namespace YHMIS.Inventory.Domain.Inventories;

public sealed partial record Inventory : IEntity<int>
{
    private Inventory(int productId, decimal stock, decimal minStock, decimal maxStock, decimal cost)
    {
        Key = productId;
        Stock = stock;
        MinStock = minStock;
        MaxStock = maxStock;
        Cost = cost;
    }
    
    /// <summary>
    /// Same as its product.
    /// </summary>
    public int Key {get; private init;} = -1;
    public decimal Stock {get; private init;}
    public decimal MinStock {get; private init;}
    public decimal MaxStock {get; private init;}
    public decimal Cost {get; private init;}
}