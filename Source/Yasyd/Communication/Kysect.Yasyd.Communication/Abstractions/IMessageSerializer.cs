using Kysect.Yasyd.Communication.Messages;

namespace Kysect.Yasyd.Communication.Abstractions;

public interface IMessageSerializer
{
    SerializedMessage Serialize<T, TRequest, TResponse>(IMessage<T, TRequest, TResponse> message);
    TResponse Deserialize<T, TRequest, TResponse>(IMessage<T, TRequest, TResponse> message, SerializedMessage serializedMessage);
}