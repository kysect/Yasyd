using Kysect.Yasyd.Communication.Abstractions;
using Kysect.Yasyd.Communication.Messages;
using Kysect.Yasyd.Communication.Models;
using Kysect.Yasyd.Communication.PackageTransport;

namespace Kysect.Yasyd.Communication.Client;

public class MessageSender<T> : IMessageSender<T> where T : struct, Enum
{
    private readonly IPackageWriter _writer;
    private readonly IMessageSerializer _serializer;
    private readonly IPackageCreator _packageCreator;

    public MessageSender(IPackageWriter writer, IMessageSerializer serializer, IPackageCreator packageCreator)
    {
        _writer = writer;
        _serializer = serializer;
        _packageCreator = packageCreator;
    }

    public TResponse Send<TRequest, TResponse>(IMessage<T, TRequest, TResponse> message)
    {
        SerializedMessage serializedMessage = _serializer.Serialize(message);
        YasydPackage package = _packageCreator.Pack(serializedMessage);
        YasydPackage responsePackage = _writer.Write(package);
        SerializedMessage response = _packageCreator.Unpack(responsePackage);
        return _serializer.Deserialize(message, response);
    }
}