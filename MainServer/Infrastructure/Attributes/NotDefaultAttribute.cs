using System.ComponentModel.DataAnnotations;

namespace MainServer.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class NotDefaultAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null) return false;
        var type = value.GetType();
        if (!type.IsValueType) return true;

        var defaultValue = Activator.CreateInstance(type);
        return !value.Equals(defaultValue);
    }
}