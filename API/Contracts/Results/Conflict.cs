namespace API;

public class Conflict : ICommandError
{
    public Conflict(string message) => this.Message = message;

    public string Message { get; }
}