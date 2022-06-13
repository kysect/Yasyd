using System.Net.Sockets;
using Kysect.Yasyd.BaseLib;
using Kysect.Yasyd.Communication.Headers;
using Kysect.Yasyd.Communication.Models;
using Kysect.Yasyd.Communication.PackageTransport;

namespace Kysect.Yasyd.Communication.Tcp;

public class TcpPackageWriter : IPackageWriter
{
    private readonly TcpClient _client;

    public TcpPackageWriter(TcpClient client)
    {
        _client = client;
    }

    public YasydPackage Write(YasydPackage package)
    {
        using (NetworkStream networkStream = _client.GetStream())
        {
            //TODO: write total size, create logic for skip full package on fail
            using (var writer = new BinaryWriter(networkStream))
            {
                writer.Write(package.Type);
                writer.Write(package.Header);
                writer.Write(package.UserHeader);
                writer.Write(package.Body);
            }

            using (var binaryReader = new BinaryReader(networkStream))
            {
                var packageType = binaryReader.ReadEnum<YasydPackageType>();
                YasydSystemHeader packageHeader = binaryReader.ReadHeader();
                string userHeader = binaryReader.ReadString();
                string body = binaryReader.ReadString();

                return new YasydPackage(packageType, packageHeader, userHeader, body);
            }
        }
    }
}