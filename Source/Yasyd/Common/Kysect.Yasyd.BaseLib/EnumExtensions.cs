namespace Kysect.Yasyd.BaseLib;

public static class EnumExtensions
{
    // TODO: rework, optimize and move to nuget
    public static int ToInt<T>(this T value) where T : struct, Enum
    {
        var o = (object)value;
        return (int) o;
    }

    public static T ToEnum<T>(this int value) where T : struct, Enum
    {
        var o = (object) value;
        return (T) o;
    }
}