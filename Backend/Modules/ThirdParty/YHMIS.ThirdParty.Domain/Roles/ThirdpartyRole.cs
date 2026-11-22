using YHMIS.Shared.Results;

namespace YHMIS.Thirdparty.Domain.Roles;

public sealed partial record ThirdpartyRole
{
    internal const int MinCodeLenth = 4;

    internal const int MaxCodeLenght = 8;

    private ThirdpartyRole(int thirdPartyId, string code)
    {
        Id = thirdPartyId;
        Code = code;
    }

    /// <summary>
    /// Third-Party Id.
    /// </summary>
    public int Id {get; private init;}

    public string Code {get; private init => field = value.Trim();}

    public bool IsActive {get; private init;}
}