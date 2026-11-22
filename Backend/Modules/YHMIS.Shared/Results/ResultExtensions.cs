namespace YHMIS.Shared.Results;

public static class ResultExtensions
{
    public static void Switch<T>(this Result<T> result, Action<T> successAction, Action<Error> failureAction)
    {
        if(result.IsSuccess)
            successAction(result.Value!);
        if(result.HasError)
            failureAction(result.Error!.Value);
    }
}