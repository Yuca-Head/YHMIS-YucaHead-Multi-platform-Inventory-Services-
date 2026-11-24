using YHMIS.Shared.Abstractions;
using YHMIS.Shared.Results;

namespace YHMIS.Thirdparty.Domain.Thirdparties;

public partial record class ThirdParty 
{
    public static class Operations
    {
        public static Result<ThirdParty> Create(int id, string name, string? ruc = null)
        {
            var rucRule = RucRule(ruc);

            if(rucRule.HasValue)
                return Result<ThirdParty>.Failure(rucRule.Value);

            var nameRule = Error.FromNameRule(name, nameof(ThirdParty));

            return nameRule.HasValue ? Result<ThirdParty>.Failure(nameRule.Value)
            : Result<ThirdParty>.Success(new ThirdParty(id, name, ruc));
        }
        
        public static Result<ThirdParty> ChangeName(
            in ThirdParty unitOfMeasure, string newName)
        {
            var nameRule = Error.FromNameRule(newName, nameof(ThirdParty));

            return nameRule.HasValue ? Result<ThirdParty>.Failure(nameRule.Value) : Result<ThirdParty>.Success(
            unitOfMeasure with { Name = newName });
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