namespace vMAPI.Extensions;

public static class BitExtensions
{
    public static bool HasFlag(this int bitmask, int flag)
    {
        return (bitmask & flag) != 0;
    }
}
