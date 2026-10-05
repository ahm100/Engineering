using System.Reflection;

namespace Financial.Application.Extensions;

public static class ReflectionExtensions
{
    public static void SetPropertyValue(this object targetObject, string propertyName, object? value)
    {
        Type? type = targetObject.GetType();
        FieldInfo? field = type?.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(_ => _.Name.Contains(propertyName)).FirstOrDefault();
        field?.SetValue(targetObject, value);
    }

    public static TOutput? GetPropertyValue<TOutput>(this object targetObject, string propertyName)
    {
        try
        {
            Type? type = targetObject?.GetType();
            var field = type?.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(_ => _.Name.Contains(propertyName)).FirstOrDefault();
            var value = field?.GetValue(targetObject);
            return (TOutput?)Convert.ChangeType(value, typeof(TOutput));
        }
        catch (InvalidCastException)
        {
            return default(TOutput);
        }

    }
}