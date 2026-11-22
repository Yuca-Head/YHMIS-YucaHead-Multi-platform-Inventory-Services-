using YHMIS.Shared.Results;
namespace YHMIS.Inventory.Domain.TransactionStatus;

public sealed partial record class TransactionStatus
{
    public static class TransactionStatusRules
    {
        public static Result<TransactionStatus> Create(int id, string name)
        {
            if (id < 0)
            return Result<TransactionStatus>.Failure(new("TransactionStatus.Invalid.Id", "Id cannot be negative"));

            var invalidName = InvalidName(name);

             if(invalidName.HasValue)
                return Result<TransactionStatus>.Failure(invalidName.Value);

                return Result<TransactionStatus>.Success(new(id, name));
        }

        public static Result<TransactionStatus> ChangeName(in TransactionStatus transactionStatus, string newName)
        {
            var invalidName = InvalidName(newName);
            if(invalidName is not null)
            return Result<TransactionStatus>.Failure(invalidName.Value);

             return Result<TransactionStatus>.Success(transactionStatus with {Name = newName});
        }

        private static Error? InvalidName(string name)
        => string.IsNullOrWhiteSpace(name) ? new("TransactionStatus.Invalid.Name", "Name cannot be empty")
        : null;
    }
}
