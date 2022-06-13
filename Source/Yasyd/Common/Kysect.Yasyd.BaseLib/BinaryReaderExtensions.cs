namespace Kysect.Yasyd.BaseLib;

public static class BinaryReaderExtensions
{
    public static T ReadEnum<T>(this BinaryReader reader) where T : struct, Enum
    {
        int value = reader.ReadInt32();
        return value.ToEnum<T>();
    }

    public static string ReadString(this BinaryReader reader)
    {
        int length = reader.ReadInt32();
        char[] chars = reader.ReadChars(length);
        return new string(chars);
    }
}