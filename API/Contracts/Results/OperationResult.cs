namespace API.Contracts.Results;

public class OperationResult<T>
{
    private OperationResult(bool isSuccess, T data, string errorMessage)
    {
        IsSuccess = isSuccess;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public bool IsSuccess { get; }
    public T Data { get; }
    public string ErrorMessage { get; }

    public static OperationResult<T> Success(T data)
    {
        return new OperationResult<T>(true, data, null);
    }

    public static OperationResult<T> Fail(string errorMessage)
    {
        return new OperationResult<T>(false, default, errorMessage);
    }
}