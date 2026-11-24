using System.Net.Mail;
using YHMIS.Shared.Results;

namespace YHMIS.Thirdparty.Domain.Contacts;

public readonly partial record struct Contact
{
    public static class Create
    {
        public static Result<Contact> Gmail(int id, int ownerId, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result<Contact>.Failure(new("Contact.Invalid.Email", "Email cannot be empty"));

            if(!MailAddress.TryCreate(email, out var _))
                return Result<Contact>.Failure(new("Contact.Invalid.Email.Format", "Invalid format for email"));

            return Result<Contact>.Success(new(id, ownerId, email, Enums.ContactType.Email));
        }

        public static Result<Contact> TelephoneNumber(int id, int ownerId, string number)
        {
            if(string.IsNullOrWhiteSpace(number))
                return Result<Contact>.Failure(new("Contact.Invalid.Telephone", "Telephone number cannot be empty"));

            if(number.Length != 8)
                return Result<Contact>.Failure(new("Contact.Invalid.Telphone.Length",
                "Telephone number lenght must be eight without format"));

            return Result<Contact>.Success(new(id, ownerId, number, Enums.ContactType.TelephoneNumber,
            $"{number[..4]}-{number[^4..]}"));      
        }
    }
}