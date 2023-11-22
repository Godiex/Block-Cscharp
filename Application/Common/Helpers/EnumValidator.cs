namespace Application.Common.Helpers;

public static class EnumValidator
{
    public static bool ValidValueForEnum<T>(string? value)
        where T : struct
    {
        T result;
        return Enum.TryParse(value, true, out result );
    }

    public static bool ValidValueForEnum<T>(IEnumerable<string> values)
        where T : struct
    {
        T result;
        return values.All(ValidValueForEnum<T>);
    }
}