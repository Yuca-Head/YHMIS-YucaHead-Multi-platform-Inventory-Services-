using YHMIS.Shared.Results;

namespace YHMIS.Thirdparty.Domain.Roles;

public sealed partial record CustomerRole
{
    public static class Operations
    {
        public static Result<CustomerRole> Create(ThirdpartyRole role)
        {
            return Result<CustomerRole>.Success(new(role));
        }
    }
}