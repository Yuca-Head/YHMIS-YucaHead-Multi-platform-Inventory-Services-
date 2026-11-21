namespace YHMIS.Shared.Results;

public readonly record struct Error(string Code, string Message)
{
    public static Error? FromNameRule(string name, string className)
    => string.IsNullOrWhiteSpace(name) ? null :
    new(Code : className+".Invalid.Name", Message : "Name cannot be empty");

}