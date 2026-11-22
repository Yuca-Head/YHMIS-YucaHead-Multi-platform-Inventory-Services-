using System.Transactions;
using YHMIS.Shared.Results;


namespace YHMIS.Inventory.Domain.InventoryMovements;

public sealed partial record class InventoryMovements
{
    public static class InventoryMovementsRules
    {
        public static Result<InventoryMovements> Create(int id, int transactionCode, DateTime date, decimal quantity, decimal productCost)
        {
            if (id < 0)
            return Result<InventoryMovements>.Failure(new("InventoryMovements.Invalied.Id", "Id cannot be negative"));
            if (transactionCode <= 0)
            return Result<InventoryMovements>.Failure(new("InventoryMovements.Invalied.TransactionCode", "Transaction code must be greater than Zero"));
            if (date == DateTime.MinValue)
            return Result<InventoryMovements>.Failure(new("InventoryMovements.Invalied.Date", "Date cannot be empty"));
             if (quantity <= 0)
            return Result<InventoryMovements>.Failure(new("InventoryMovements.Invalied.Quantity", "Quantity must be greater than Zero"));
            if (productCost < 0)
            return Result<InventoryMovements>.Failure(new("InventoryMovements.Invalied.ProductCost", "Product Cost cannot be negative"));

            return Result<InventoryMovements>.Success(new(id, transactionCode, date, quantity, productCost));
        }

    }
}
