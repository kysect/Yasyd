using Kysect.Yasyd.BaseLib;

namespace Kysect.Yasyd.Communication.Messages;

public interface INoResponseMessage<TType, TRequest> : IMessage<TType, TRequest, Unit>
{
}

public interface IVoidMessage<TType> : INoResponseMessage<TType, Unit>
{
}