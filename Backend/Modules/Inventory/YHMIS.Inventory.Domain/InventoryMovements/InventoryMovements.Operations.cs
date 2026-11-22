using System.Transactions;
using YHMIS.Inventory.Domain.Enums;
using YHMIS.Shared.Results;
using TransactionStatus = YHMIS.Inventory.Domain.Enums.TransactionStatus;



namespace YHMIS.Inventory.Domain.InventoryMovements;

public sealed partial record class InventoryMovement
{
    public static class Operations
    {
        public static Result<InventoryMovement> Create(int id, int transactionCode, DateTimeOffset date, SourceTransaction source,
        int quantity, decimal productCost, TransactionStatus? status = null)
        {
            if (id < 0)
                return Result<InventoryMovement>.Failure(new("InventoryMovement.Invalid.Id", "Id cannot be negative"));
            if (transactionCode <= 0)
                return Result<InventoryMovement>.Failure(new("InventoryMovement.Invalid.TransactionCode", "Transaction code must be greater than Zero"));
            if (date == DateTime.MinValue)
                return Result<InventoryMovement>.Failure(new("InventoryMovement.Invalid.Date", "Date cannot be empty"));
            if (quantity <= 0)
                return Result<InventoryMovement>.Failure(new("InventoryMovement.Invalid.Quantity", "Quantity must be greater than Zero"));
            if (productCost < 0)
                return Result<InventoryMovement>.Failure(new("InventoryMovement.Invalid.ProductCost", "Product Cost cannot be negative"));

            return Result<InventoryMovement>.Success(new(id, transactionCode, date, source, quantity, productCost)
            {Status = status ?? TransactionStatus.Draft});
        }


        public static Result<InventoryMovement> CancelateMovement(in InventoryMovement movement)
        => ChangeStatus(movement, TransactionStatus.Canceled);

        public static Result<InventoryMovement> CompleteMovement(in InventoryMovement movement)
        => ChangeStatus(movement, TransactionStatus.Completed);

        private static Result<InventoryMovement> ChangeStatus(InventoryMovement movement, TransactionStatus newStatus)
        {
            var invalidState = DefaultStatusForChangingRule(movement);

            if(invalidState.HasValue)
                return Result<InventoryMovement>.Failure(invalidState.Value);
            
            return Result<InventoryMovement>.Success(movement with {Status = newStatus});
        }

        private static Error? DefaultStatusForChangingRule(InventoryMovement movement)
        => movement.Status is TransactionStatus.Draft ? null : 
        new("InventoryMovement.InvalidOperation.State", "Movement status can only change when is draft.");
    }
}
