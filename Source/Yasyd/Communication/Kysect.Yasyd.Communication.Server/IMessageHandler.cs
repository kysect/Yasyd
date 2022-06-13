using Kysect.Yasyd.Communication.Messages;

namespace Kysect.Yasyd.Communication.Server;

public interface IMessageHandler<T> where T : struct, Enum
{
    SerializedMessageResponse Handle(SerializedMessage message);
}