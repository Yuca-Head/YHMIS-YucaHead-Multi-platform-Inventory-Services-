namespace YHMIS.Inventory.Domain.Inventories;

public sealed partial record Inventory
{
    private Inventory(int productId, decimal stock, decimal minStock, decimal maxStock, decimal cost)
    {
        ProductId = productId;
        Stock = stock;
        MinStock = minStock;
        MaxStock = maxStock;
        Cost = cost;
    }

    public int ProductId {get; private init;} = -1;
    public decimal Stock {get; private init;}
    public decimal MinStock {get; private init;}
    public decimal MaxStock {get; private init;}
    public decimal Cost {get; private init;}
}