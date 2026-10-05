namespace Engineering.Application.Extensions.CustomValidations;

public static class FluentValidationHelper
{
    private static bool IsDistinct<TSource, TResult>(this IEnumerable<TSource> elements,
        Func<TSource, TResult> selector)
    {
        var hashSet = new HashSet<TResult>();
        return elements.Select(selector).All(element => hashSet.Add(element));
    }

    public static void Unique<T, TSource, TResult>(
        this IRuleBuilder<T, IEnumerable<TSource>> ruleBuilder,
        Func<TSource, TResult> selector, string? propertyName = null)
    {
        if (selector == null)
            throw new ArgumentNullException(nameof(selector), @"Cannot pass a null selector.");

        ruleBuilder
            .Must(x => x.IsDistinct(selector))
            .OverridePropertyName(propertyName ?? nameof(selector))
            .WithErrorCode("UniqueValidator");
    }
}