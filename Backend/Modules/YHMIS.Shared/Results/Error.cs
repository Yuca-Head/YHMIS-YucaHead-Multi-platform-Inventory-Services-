namespace YHMIS.Shared.Results;

public readonly record struct Error(string Code, string Message, Exception? Exception = null)
{
    public bool HasException => Exception is not null;


















    #region Static methods

    public static Error? FromNameRule(string name, string className)
    => string.IsNullOrWhiteSpace(name) ? null :
    new(Code : className+".Invalid.Name", Message : "Name cannot be empty");

    public static Error? FromCodeRule(string code, int lenght, string className)
    {
        if(string.IsNullOrWhiteSpace(code))
            return new(Code : className+".Invalid.Code", Message : "Code cannot be empty");
        
        if(code.Length != lenght)
            return new(Code : className+".Invalid.Code.Lenght", Message : $"Code lenght must be {lenght}");
        
        return null;
    } 

    public static Error? FromCodeRule(string code, int minLenght, int maxLenght, string className)
    {
        if(string.IsNullOrWhiteSpace(code))
            return new(Code : className+".Invalid.Code", Message : "Code cannot be empty");
        
        if(code.Length < minLenght || code.Length > maxLenght)
            return new(Code : className+".Invalid.Code.Lenght", 
            Message : $"Code lenght must be in the range: {minLenght}-{maxLenght}");
        
        return null;
    }

    public static Error? FromOperation(Action action, string code)
    {
        try
        {
            action();
            return null;
        }catch (Exception e)
        {
            return new Error(code, e.Message, e);
        }
    }

    #endregion
}