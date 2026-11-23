using YHMIS.Inventory.Domain.Enums;
using YHMIS.Shared.Abstractions;

namespace YHMIS.Inventory.Domain.InventoryMovements;

public sealed partial record InventoryMovement : IEntity<int>
{
    private InventoryMovement(int id, int transactionCode, 
    DateTimeOffset date, SourceTransaction source, int quantity, decimal productCost)
    {
        Key = id;
        TransactionCode = transactionCode;
        SourceTransaction = source;
        Quantity = quantity;
        Cost = productCost;
        Date = date;
    }
    
    public int Key{get; private init;} = -1;
    public int TransactionCode { get; private init;}
    public SourceTransaction SourceTransaction {get; private init;}
    public TransactionStatus Status {get; private init;}
    public DateTimeOffset Date {get; private init;}
    public int Quantity {get; private init;}
    public decimal Cost {get; private init;}
}
