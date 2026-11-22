using YHMIS.Shared.Results;
namespace YHMIS.Inventory.Domain.SourceTransactionCode;

public sealed partial record class SourceTransactionCode
{
    public static class SourceTransactionCodeRules
    {
        public static Result<SourceTransactionCode> Create(int id, string name)
        {
            if (id < 0)
            return Result<SourceTransactionCode>.Failure(new("SourceTransactionCode.Invalid.Id", "Id cannot be negative"));

            var invalidName = InvalidName(name);

             if(invalidName.HasValue)
                return Result<SourceTransactionCode>.Failure(invalidName.Value);

                return Result<SourceTransactionCode>.Success(new(id, name));
        }

        public static Result<SourceTransactionCode> ChangeName(in SourceTransactionCode sourceTransactionCode, string newName)
        {
            var invalidName = InvalidName(newName);
            if(invalidName is not null)
            return Result<SourceTransactionCode>.Failure(invalidName.Value);

             return Result<SourceTransactionCode>.Success(sourceTransactionCode with {Name = newName});
        }

        private static Error? InvalidName(string name)
        => string.IsNullOrWhiteSpace(name) ? new("SourceTransactionCode.Invalid.Name", "Name cannot be empty")
        : null;
    }
}
