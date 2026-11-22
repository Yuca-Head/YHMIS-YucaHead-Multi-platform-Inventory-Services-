using YHMIS.Shared.Results;

namespace YHMIS.Inventory.Domain.Inventories;

public sealed partial record class Inventory
{
    public static class InventoryRules
    {
        public static Result<Inventory> Create(int productId, decimal stock, decimal minStock, decimal maxStock, decimal cost)
        {
            if (productId < 0)
                return Result<Inventory>.Failure(new("Inventory.Invalid.Id", "ProductId cannot be negative"));
            if (stock < 0)
                return Result<Inventory>.Failure(new("Inventory.Invalid.Stock", "Stock cannot be negative"));
            if (minStock < 0)
                return Result<Inventory>.Failure(new("Inventory.Invalid.MinStock", "Minimum Stock cannot be negative"));
            if (maxStock < 0)
                return Result<Inventory>.Failure(new("Inventory.Invalid.MaxStock", "Maximun Stock cannot be negative"));
            if (cost < 0)
                return Result<Inventory>.Failure(new("Inventory.Invalid.Cost", "Cost cannot be negative"));
            if (minStock > maxStock)
                return Result<Inventory>.Failure(new("Inventory.Invalid.StockLimits", "Minimun Stock cannot exceed maximun Stock"));

            return Result<Inventory>.Success(new(productId, stock, minStock, maxStock, cost));
        }
    }
}