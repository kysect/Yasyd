namespace Kysect.Yasyd.BaseLib;

public static class BinaryWriterExtensions
{
    public static void Write<T>(this BinaryWriter writer, T value) where T : struct, Enum
    {
        writer.Write(value.ToInt());
    }

    public static void Write(this BinaryWriter writer, string value)
    {
        writer.Write(value.Length);
        writer.Write(value.ToCharArray());
    }
}