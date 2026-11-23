using YHMIS.Shared.Abstractions;
using YHMIS.Shared.Results;

namespace YHMIS.Thirdparty.Domain.Thirdparties;

public partial record class ThirdParty 
{
    public static class Operations
    {
        public static Result<ThirdParty> Create(int id, string name)
        {
            if (id < 0)
                return Result<ThirdParty>.Failure(
                    new Error("ThirdParty.Invalid.Id", "Id cannot be negative"));


            var nameRule = Error.FromNameRule(name, nameof(ThirdParty));

            return nameRule.HasValue ? Result<ThirdParty>.Failure(nameRule.Value)
            : Result<ThirdParty>.Success(new ThirdParty(id, name));
        }
        
        public static Result<ThirdParty> ChangeName(
            in ThirdParty unitOfMeasure, string newName)
        {
            var nameRule = Error.FromNameRule(newName, nameof(ThirdParty));

            return nameRule.HasValue ? Result<ThirdParty>.Failure(nameRule.Value) : Result<ThirdParty>.Success(
            unitOfMeasure with { Name = newName });
        }
    }
}