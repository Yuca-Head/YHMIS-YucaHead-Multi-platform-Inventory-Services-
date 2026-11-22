namespace YHMIS.Inventory.Domain.TransactionStatus;

public sealed partial record class TransactionStatus
{
    private TransactionStatus(int id, string name)
    {
        Id = id;
        Name = name;
    }
    private TransactionStatus(){}
    public int Id { get; private init; } = -1;
    public string? Name {get; private init => field = value?.Trim();} = "";
}
