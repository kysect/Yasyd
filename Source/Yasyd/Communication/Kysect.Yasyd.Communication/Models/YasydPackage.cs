using Kysect.Yasyd.Communication.Headers;

namespace Kysect.Yasyd.Communication.Models;

public record struct YasydPackage(YasydPackageType Type, YasydSystemHeader Header, string UserHeader, string Body);