using Kysect.Yasyd.Communication.Headers;
using Kysect.Yasyd.Communication.Messages;
using Kysect.Yasyd.Communication.Models;

namespace Kysect.Yasyd.Communication.PackageTransport;

public interface IPackageCreator
{
    YasydPackage Pack(SerializedMessage message);
    SerializedMessage Unpack(YasydPackage package);
}

public class HeaderlessPackageCreator : IPackageCreator
{
    public YasydPackage Pack(SerializedMessage message)
    {
        return new YasydPackage(
            YasydPackageType.UserRequest,
            YasydSystemHeader.Empty,
            message.Header,
            message.Body);
    }

    public SerializedMessage Unpack(YasydPackage package)
    {
        return new SerializedMessage(package.UserHeader, package.Body);
    }
}