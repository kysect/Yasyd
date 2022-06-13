using Kysect.Yasyd.BaseLib;
using Kysect.Yasyd.Communication.Headers;

namespace Kysect.Yasyd.Communication.Tcp;

public static class YasydPackageReadWriteExtensions
{
    public static void Write(this BinaryWriter writer, YasydSystemHeader header)
    {
        IReadOnlyCollection<KeyValuePair<string, string>> pairs = header.EnumerateHeaders();
        writer.Write(pairs.Count);
        foreach (KeyValuePair<string, string> pair in pairs)
        {
            writer.Write(pair.Key);
            writer.Write(pair.Value);
        }
    }

    public static YasydSystemHeader ReadHeader(this BinaryReader reader)
    {
        var systemHeader = new YasydSystemHeader();
        int pairCount = reader.ReadInt32();

        for (int i = 0; i < pairCount; i++)
        {
            string key = reader.ReadString();
            string value = reader.ReadString();
            systemHeader.Add(key, value);
        }

        return systemHeader;
    }
}