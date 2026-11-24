using YHMIS.Shared.Results;

namespace YHMIS.Thirdparty.Domain.Roles.SupplierRole;

public sealed partial record SupplierRole
{
    public static class Operations
    {
        public static Result<SupplierRole> Create(ThirdpartyRole role, string? ruc)
        {
            return Result<SupplierRole>.Success(new(role));
        }


    }
}