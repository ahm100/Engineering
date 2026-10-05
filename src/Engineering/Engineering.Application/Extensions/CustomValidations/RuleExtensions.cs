namespace Engineering.Application.Extensions.CustomValidations;

public static class RuleExtensions
{
    public static IRuleBuilderOptions<T, List<TItem>?> HasNoDuplicates<T, TItem, TKey>(
        this IRuleBuilder<T, List<TItem>?> ruleBuilder,
        Func<TItem, TKey> keySelector,
        string propertyName)
    {
        return ruleBuilder
            .Must(items => items?.GroupBy(keySelector).All(g => g.Count() == 1) ?? true)
                .WithError(GlobalErrors.NoDuplicates(propertyName));
    }

    public static IRuleBuilderOptions<T, TProperty> IsPositive<T, TProperty>(
        this IRuleBuilder<T, TProperty> rule,
        string propertyName)
        where TProperty : struct, IComparable<TProperty>
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .Must(value => value.CompareTo(default) > 0)
                .WithError(GlobalErrors.RequiredGreaterThanZero(propertyName));
    }

    public static IRuleBuilderOptions<T, int> PageIndexZero<T>(
        this IRuleBuilder<T, int> rule,
        string propertyName)
    {
        return rule
            .GreaterThanOrEqualTo(GlobalErrors.Zero)
            .WithError(GlobalErrors.GreaterThanOrEqualTo(propertyName))
            .LessThanOrEqualTo(GlobalErrors.MaxIndex)
            .WithError(GlobalErrors.LessThanOrEqualTo(propertyName, GlobalErrors.MaxIndex));
    }

    public static IRuleBuilderOptions<T, int> PageIndexOne<T>(
        this IRuleBuilder<T, int> rule,
        string propertyName)
    {
        return rule
            .GreaterThanOrEqualTo(GlobalErrors.One)
            .WithError(GlobalErrors.GreaterThanOrEqualTo(propertyName))
            .LessThanOrEqualTo(GlobalErrors.MaxIndex)
            .WithError(GlobalErrors.LessThanOrEqualTo(propertyName, GlobalErrors.MaxIndex));
    }

    public static IRuleBuilderOptions<T, int> PageSizeZero<T>(
        this IRuleBuilder<T, int> rule,
        string propertyName)
    {
        return rule
            .GreaterThanOrEqualTo(GlobalErrors.Zero)
            .WithError(GlobalErrors.GreaterThanOrEqualTo(propertyName))
            .LessThanOrEqualTo(GlobalErrors.MaxIndex)
            .WithError(GlobalErrors.LessThanOrEqualTo(propertyName, GlobalErrors.MaxSize));
    }

    public static IRuleBuilderOptions<T, TProperty?> IsPositiveWithNullableInput<T, TProperty>(
    this IRuleBuilder<T, TProperty?> rule,
    string propertyName)
    where TProperty : struct, IComparable<TProperty>
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .Must(value => value.HasValue && value.Value.CompareTo(default) > 0)
                .WithError(GlobalErrors.RequiredGreaterThanZero(propertyName));
    }

    public static IRuleBuilderOptions<T, TProperty?> IsOptionalPositive<T, TProperty>(
    this IRuleBuilder<T, TProperty?> rule,
    string propertyName)
    where TProperty : struct, IComparable<TProperty>
    {
        return rule
            .Must(value => !value.HasValue || value.Value.CompareTo(default) > 0)
                .WithError(GlobalErrors.RequiredGreaterThanZero(propertyName));
    }

    public static IRuleBuilderOptions<T, string?> IsFullString<T>(
        this IRuleBuilder<T, string?> rule,
        string propertyName,
        int maxLength,
        string regex,
        string regexName)
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .MaximumLength(maxLength).WithError(GlobalErrors.RequiredMaxLength(propertyName, maxLength))
            .Matches(regex!).WithError(GlobalErrors.RequiredRegex(propertyName, regexName));
    }

    public static IRuleBuilderOptions<T, string?> HasMaxLength<T>(
        this IRuleBuilder<T, string?> rule,
        string propertyName,
        int maxLength)
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .MaximumLength(maxLength).WithError(GlobalErrors.RequiredMaxLength(propertyName, maxLength));
    }

    public static IRuleBuilderOptions<T, string?> MatchesPattern<T>(
        this IRuleBuilder<T, string?> rule,
        string propertyName,
        string regex,
        string regexName)
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .Matches(regex!).WithError(GlobalErrors.RequiredRegex(propertyName, regexName));
    }

    public static IRuleBuilderOptions<T, string?> IsRequiredString<T>(
        this IRuleBuilder<T, string?> rule,
        string propertyName)
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName));
    }

    public static IRuleBuilderOptions<T, decimal> IsRequiredDecimal<T>(
        this IRuleBuilder<T, decimal> rule,
        string propertyName)
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName));
    }

    public static IRuleBuilderOptions<T, bool> IsRequiredBool<T>(
        this IRuleBuilder<T, bool> rule,
        string propertyName)
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName));
    }

    public static IRuleBuilderOptions<T, int> IsRequiredInt<T>(
        this IRuleBuilder<T, int> rule,
        string propertyName)
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName));
    }

    public static IRuleBuilderOptions<T, TEnum> IsEnum<T, TEnum>(
        this IRuleBuilder<T, TEnum> rule,
        string propertyName)
        where TEnum : struct, Enum
    {
        return rule
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .Must(value => Enum.IsDefined(typeof(TEnum), value))
            .WithError(GlobalErrors.InvalidEnumValue(propertyName));
    }

    public static IRuleBuilderOptions<T, TEnum?> IsNullableEnum<T, TEnum>(
    this IRuleBuilder<T, TEnum?> rule,
    string propertyName)
    where TEnum : struct, Enum
    {
        return rule
            .Must(value => !value.HasValue ||
                           Enum.IsDefined(typeof(TEnum), value.Value))
            .WithError(GlobalErrors.InvalidEnumValue(propertyName));
    }

    public static IRuleBuilderOptions<T, DateTime> IsDate<T>(
        this IRuleBuilder<T, DateTime> rule,
        string propertyName)
    {
        return rule
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName));
    }

    public static IRuleBuilderOptions<T, DateTime> IsTodayDate<T>(
        this IRuleBuilder<T, DateTime> rule,
        string propertyName)
    {
        return rule
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .Must(date => date.Date == DateTime.Today)
            .WithError(GlobalErrors.MustBeToday(propertyName));
    }
    public static IRuleBuilderOptions<T, DateTime> GreaterThanPropertyDate<T>(
        this IRuleBuilder<T, DateTime> rule,
        Func<T, DateTime> fromPropertySelector,
        string propertyName,
        string compareToName)
    {
        return rule
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .Must((model, toDate) => toDate > fromPropertySelector(model))
            .WithError(GlobalErrors.MustBeGreaterThan(propertyName, compareToName));
    }
    public static IRuleBuilderOptions<T, DateTime> LessThanPropertyDate<T>(
        this IRuleBuilder<T, DateTime> rule,
        Func<T, DateTime> toPropertySelector,
        string propertyName,
        string compareToName)
    {
        return rule
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName))
            .Must((model, fromDate) => fromDate < toPropertySelector(model))
            .WithError(GlobalErrors.MustBeLessThan(propertyName, compareToName));
    }

    public static IRuleBuilderOptions<T, TProperty> IsEntity<T, TProperty>(
        this IRuleBuilder<T, TProperty> rule,
        string propertyName)
    {
        return rule
            .NotEmpty().WithError(GlobalErrors.RequiredEmpty(propertyName))
            .NotNull().WithError(GlobalErrors.RequiredNull(propertyName));
    }

    public static bool HasOnlyValidOrderFields<T>(
    string[]? orderBy)
    {
        if (orderBy is null || orderBy.Length == 0)
            return true;

        var properties = typeof(T)
            .GetProperties()
            .Select(x => x.Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var item in orderBy)
        {
            if (string.IsNullOrWhiteSpace(item))
                return false;

            var field = item
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault();

            if (field is null || !properties.Contains(field))
                return false;
        }

        return true;
    }

    public static IRuleBuilderOptions<T, string?> HasOptionalMaxLength<T>(
    this IRuleBuilder<T, string?> rule,
    string propertyName,
    int maxLength)
    {
        return rule
            .MaximumLength(maxLength)
            .WithError(GlobalErrors.RequiredMaxLength(propertyName, maxLength));
    }

    public static IRuleBuilderOptions<T, DateTime?> IsOptionalDateGreaterThanOrEqualTo<T>(
        this IRuleBuilder<T, DateTime?> rule,
        Func<T, DateTime> compareToSelector,
        string propertyName,
        string compareToName)
    {
        return rule
            .Must((model, date) => !date.HasValue || date.Value >= compareToSelector(model))
            .WithError(GlobalErrors.MustBeGreaterThanOrEqualTo(propertyName, compareToName));
    }

    public static IRuleBuilderOptions<T, DateTime?> IsOptionalDateGreaterThan<T>(
        this IRuleBuilder<T, DateTime?> rule,
        Func<T, DateTime> compareToSelector,
        string propertyName,
        string compareToName)
    {
        return rule
            .Must((model, date) => !date.HasValue || date.Value > compareToSelector(model))
            .WithError(GlobalErrors.MustBeGreaterThan(propertyName, compareToName));
    }

    public static IRuleBuilderOptions<T, DateTime?> IsOptionalDateLessThan<T>(
        this IRuleBuilder<T, DateTime?> rule,
        Func<T, DateTime> compareToSelector,
        string propertyName,
        string compareToName)
    {
        return rule
            .Must((model, date) => !date.HasValue || date.Value < compareToSelector(model))
            .WithError(GlobalErrors.MustBeLessThan(propertyName, compareToName));
    }

    public static IRuleBuilderOptions<T, TProperty> IsBetween<T, TProperty>(
    this IRuleBuilder<T, TProperty> rule,
    TProperty min,
    TProperty max,
    string propertyName)
    where TProperty : struct, IComparable<TProperty>
    {
        return rule
            .Must(value => value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0)
            .WithError(GlobalErrors.MustBeBetween(propertyName, min, max));
    }
}
