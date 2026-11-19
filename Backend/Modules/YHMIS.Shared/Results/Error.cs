namespace YHMIS.Shared.Results;

public readonly record struct Error(string Code, string Message);