namespace API;

public class DbError : ICommandError
{
    public DbError(string message, string details)
    {
        this.Message = message;
        this.Details = details;
    }

    public string Message { get; }

    public string Details { get; }
}