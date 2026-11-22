namespace YHMIS.Inventory.Domain.Enums;

public enum SourceTransaction
{
    Purchase = 1,
    Sale = 2,
    Adjust = 3,
}

public enum TransactionStatus
{
    Draft = 1,
    Completed = 2,
    Canceled = 3
}