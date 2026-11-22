namespace YHMIS.Inventory.Domain.InventoryMovements;

public sealed partial record class InventoryMovements
{
    public InventoryMovements(int id, int transactionCode, DateTime date, decimal quantity, decimal productCost)
    {
        Id = id;
        TransactionCode = transactionCode;
        Quantity = quantity;
        ProductCost = productCost;
        Date = date;
    }
    public InventoryMovements(){}
    public int Id{get; private init;} = -1;
    public int TransactionCode { get; private init;}
    public DateTime Date {get; private init;}
    public decimal Quantity {get; private init;}
    public decimal ProductCost {get; private init;}
}
