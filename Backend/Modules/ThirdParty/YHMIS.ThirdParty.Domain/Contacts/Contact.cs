using YHMIS.Thirdparty.Domain.Enums;

namespace YHMIS.Thirdparty.Domain.Contacts;

public readonly partial record struct Contact
{

    private Contact(int id, int ownerId, string value, ContactType type)
    {
        Id = id;
        OwnerId = ownerId;
        Value = value;
        Type = type;
    }
    public readonly int Id {get; private init;}
    public readonly int OwnerId {get; private init;}
    public readonly string Value {get; private init;}
    public readonly ContactType Type {get; private init;}

    /// <summary>
    /// Returns contact value with a standar format.
    /// </summary>
    public readonly string GetFormat {get; private init;}
}