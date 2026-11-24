using YHMIS.Inventory.Domain.Enums;

namespace YHMIS.Inventory.Domain.InventoryMovements;

public sealed partial record InventoryMovement
{
    private InventoryMovement(int id, int transactionCode, 
    DateTimeOffset date, TransactionSource source, int quantity, decimal productCost)
    {
        Id = id;
        TransactionCode = transactionCode;
        SourceTransaction = source;
        Quantity = quantity;
        Cost = productCost;
        Date = date;
    }
    
    public int Id{get; private init;} = -1;
    public int TransactionCode { get; private init;}
    public TransactionSource SourceTransaction {get; private init;}
    public TransactionStatus Status {get; private init;}
    public DateTimeOffset Date {get; private init;}
    public int Quantity {get; private init;}
    public decimal Cost {get; private init;}
}
