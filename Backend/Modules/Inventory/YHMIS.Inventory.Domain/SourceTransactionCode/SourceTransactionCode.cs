namespace YHMIS.Inventory.Domain.SourceTransactionCode;

public sealed partial record class SourceTransactionCode
{
    private SourceTransactionCode(int id, string name)
    {
        Id = id;
        Name = name;
    }
    private SourceTransactionCode(){}
    public int Id { get; private init; } = -1;
    public string? Name {get; private init => field = value?.Trim();} = "";
}
