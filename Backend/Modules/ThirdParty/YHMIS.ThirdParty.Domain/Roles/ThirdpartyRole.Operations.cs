using YHMIS.Shared.Results;

namespace YHMIS.Thirdparty.Domain.Roles;

public sealed partial record ThirdpartyRole
{
    public static class Operations
    {
        public static Result<ThirdpartyRole> Create(int id, string code)
        {
            var codeRule = Error.FromCodeRule(code, MinCodeLenth, MaxCodeLenght, nameof(ThirdpartyRole));

            if(codeRule.HasValue)
                return Result<ThirdpartyRole>.Failure(codeRule.Value);

            return Result<ThirdpartyRole>.Success(new(id, code));
        }

        public static ThirdpartyRole ChangeState(ThirdpartyRole role, bool isActive)
        => role with {IsActive = isActive};
    }   
}