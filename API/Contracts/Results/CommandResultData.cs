namespace API;

public class CommandResultData
{
    public object Id { get; init; }

    public static CommandResultData Get(object id)
    {
        return new CommandResultData
        {
            Id = id
        };
    }
}