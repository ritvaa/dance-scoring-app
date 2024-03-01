namespace API;

public interface ICommandResult
{
    bool Ok { get; }

    object Data { get; }

    ICommandError ErrorDetails { get; }
}