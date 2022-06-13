namespace Kysect.Yasyd.Communication.Messages;

//TODO: message spec
public interface IMessage<TType, TRequest, TResponse>
{
    static abstract TType CommandIdentifier { get; }

    TRequest MessageRequest { get; }
}