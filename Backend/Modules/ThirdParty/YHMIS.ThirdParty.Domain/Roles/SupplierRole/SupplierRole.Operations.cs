using YHMIS.Shared.Results;

namespace YHMIS.Thirdparty.Domain.Roles.SupplierRole;

public sealed partial record SupplierRole
{
    public static class Operations
    {
        public static Result<SupplierRole> Create(ThirdpartyRole role, string? ruc)
        {
            var rucRule = RucRule(ruc);

            if(rucRule.HasValue)
                return Result<SupplierRole>.Failure(rucRule.Value);

            return Result<SupplierRole>.Success(new(role, ruc));
        }

        private static Error? RucRule(string? ruc)
        {
            if(ruc is null)
                return null;

            if(ruc.Length != RucLenth)
                return new Error("SupplierRole.Invalid.RUC.Lenth", $"RUC lenght must be {RucLenth}");
            
            if(!char.IsLetter(ruc.First()))
                return new Error("SupplierRole.Invalid.RUC", "RUC first character must be a letter");
            
            return null;
        }
    }
}