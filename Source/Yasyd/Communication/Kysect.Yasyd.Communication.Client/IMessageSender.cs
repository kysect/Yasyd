using Kysect.Yasyd.Communication.Messages;

namespace Kysect.Yasyd.Communication.Client;

public interface IMessageSender<T> where T : struct, Enum
{
    TResponse Send<TRequest, TResponse>(IMessage<T, TRequest, TResponse> message);
}