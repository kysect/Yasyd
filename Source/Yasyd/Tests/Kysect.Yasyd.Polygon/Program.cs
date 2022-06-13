// See https://aka.ms/new-console-template for more information

using Kysect.Yasyd.BaseLib;
using Kysect.Yasyd.Communication.Messages;

Console.WriteLine("Hello, World!");

public record struct CommandIdentifier(int Scope, int Type)
{
    public static CommandIdentifier From<TScope, TType>(TScope scope, TType type)
        where TScope : struct, Enum
        where TType : struct, Enum
    {
        return new CommandIdentifier(scope.ToInt(), type.ToInt());
    }
}

public enum MessageScope
{
    Debug = 1,
}

public enum MessageType
{
    Ping = 1,
}

public class PingCommand : IVoidMessage<CommandIdentifier>
{
    public static CommandIdentifier CommandIdentifier => CommandIdentifier.From(MessageScope.Debug, MessageType.Ping);
    public Unit MessageRequest => Unit.Instance;
}