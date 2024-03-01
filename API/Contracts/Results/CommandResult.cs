namespace API;

#nullable disable

    public sealed class CommandResult : ICommandResult
    {
        private CommandResult()
        {
        }

        public bool Ok { get; private set; }

        public ICommandError ErrorDetails { get; private set; }

        public object Data { get; private set; }

        public static CommandResult Success()
        {
            return new CommandResult() { Ok = true };
        }

        public static CommandResult Success(object data)
        {
            return new CommandResult() { Ok = true, Data = data };
        }

        public static CommandResult Fail(ICommandError commandError)
        {
            return new CommandResult()
            {
                Ok = false,
                ErrorDetails = commandError
            };
        }
    }

