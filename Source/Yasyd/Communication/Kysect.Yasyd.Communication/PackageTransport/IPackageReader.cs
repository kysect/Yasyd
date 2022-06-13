using Kysect.Yasyd.Communication.Models;

namespace Kysect.Yasyd.Communication.PackageTransport;

public interface IPackageReader
{
    YasydPackage Read();
}