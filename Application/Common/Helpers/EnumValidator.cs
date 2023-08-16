namespace Application.Common.Helpers;

public static class EnumValidator
{
    public static bool ValidValueForEnum<T>(string? value)
        where T : struct
    {
        T result;
        return Enum.TryParse(value, true, out result );
    }

    public static bool ValidValueForEnum<T>(List<string>? values)
        where T : struct
    {
        T result;
        foreach (var x in values)
        {
            if (!ValidValueForEnum<T>(x)) return false;
        }

        return true;
    }
}