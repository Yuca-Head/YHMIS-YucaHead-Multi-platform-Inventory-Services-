namespace YHMIS.Shared.Results;

public record class Result<T>
{
    internal Result()
    {
    }
    
    public T? Value {get; init;} = default;
    public Error? Error {get; init;} = default;
    public bool HasError => !IsSuccess;
    public bool IsSuccess {get; private init;} = false;


    /// <summary>
    /// Operation was completed successfully.
    /// </summary>
    /// <returns>Result with an entity.</returns>
    public static Result<T> Success(T entity)
    => new(){Value = entity, IsSuccess = true};

    /// <summary>
    /// Operation fails.
    /// </summary>
    /// <param name="error">Error that raises failure.</param>
    /// <returns>Result with an error.</returns>
    public static Result<T> Failure(Error error)
    => new() {Error = error, IsSuccess = false}; 
}