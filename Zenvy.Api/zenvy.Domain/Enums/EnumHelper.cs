namespace zenvy.Domain.Enums;

/// <summary>
/// Enum Helper - provides utility methods for enum conversions
/// Used for converting between enum values and their string/numeric representations for database operations
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// Convert enum value to its string representation (name)
    /// </summary>
    public static string ToStringValue<T>(T enumValue) where T : struct, Enum
    {
        return enumValue.ToString();
    }

    /// <summary>
    /// Convert string to enum value (case-insensitive)
    /// </summary>
    public static T FromStringValue<T>(string value) where T : struct, Enum
    {
        if (Enum.TryParse<T>(value, ignoreCase: true, out var result))
        {
            return result;
        }
        throw new ArgumentException($"'{value}' is not a valid value for enum type {typeof(T).Name}", nameof(value));
    }

    /// <summary>
    /// Try convert string to enum value (case-insensitive)
    /// </summary>
    public static bool TryFromStringValue<T>(string value, out T result) where T : struct, Enum
    {
        return Enum.TryParse<T>(value, ignoreCase: true, out result);
    }

    /// <summary>
    /// Get all valid values for an enum type as strings
    /// </summary>
    public static List<string> GetAllValues<T>() where T : struct, Enum
    {
        return Enum.GetNames(typeof(T)).ToList();
    }

    /// <summary>
    /// Check if string is a valid enum value
    /// </summary>
    public static bool IsValidEnumValue<T>(string value) where T : struct, Enum
    {
        return Enum.TryParse<T>(value, ignoreCase: true, out _);
    }
}
