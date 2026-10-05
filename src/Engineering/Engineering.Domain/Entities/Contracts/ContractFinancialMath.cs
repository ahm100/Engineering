using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

public static class ContractFinancialMath
{
    public const int MoneyPrecision = 18;
    public const int MoneyScale = 2;
    public const int QuantityPrecision = 18;
    public const int QuantityScale = 5;
    public const int PercentagePrecision = 5;
    public const int PercentageScale = 2;
    public const int ChangeValuePrecision = 23;
    public const int ChangeValueScale = 5;
    public const int RatePrecision = 18;
    public const int RateScale = 6;
    public const int DurationPrecision = 18;
    public const int DurationScale = 2;

    private const MidpointRounding Rounding = MidpointRounding.AwayFromZero;

    public static decimal NormalizeMoney(decimal value) =>
        Math.Round(value, MoneyScale, Rounding);

    public static decimal NormalizeQuantity(decimal value) =>
        Math.Round(value, QuantityScale, Rounding);

    public static decimal NormalizePercentage(decimal value) =>
        Math.Round(value, PercentageScale, Rounding);

    public static decimal NormalizeChangeValue(decimal value) =>
        Math.Round(value, ChangeValueScale, Rounding);

    public static decimal NormalizeRate(decimal value) =>
        Math.Round(value, RateScale, Rounding);

    public static decimal NormalizeDuration(decimal value) =>
        Math.Round(value, DurationScale, Rounding);

    public static decimal? CalculateContractTypeDetailAmount(
        PricingMethod pricingMethod,
        decimal quantity,
        decimal? unitPrice,
        decimal? fixedAmount,
        decimal? duration)
    {
        quantity = NormalizeQuantity(quantity);
        unitPrice = unitPrice.HasValue
            ? NormalizeMoney(unitPrice.Value)
            : null;
        fixedAmount = fixedAmount.HasValue
            ? NormalizeMoney(fixedAmount.Value)
            : null;
        duration = duration.HasValue
            ? NormalizeDuration(duration.Value)
            : null;

        var amount = pricingMethod switch
        {
            PricingMethod.LumpSum => fixedAmount,
            PricingMethod.UnitPrice or PricingMethod.CostPlus =>
                unitPrice.HasValue
                    ? quantity * unitPrice.Value
                    : null,
            PricingMethod.TimeAndMaterial =>
                unitPrice.HasValue
                    ? (quantity * unitPrice.Value) +
                      ((duration ?? 0m) * unitPrice.Value)
                    : null,
            _ => throw new InvalidOperationException(
                $"PricingMethod {pricingMethod} is not supported.")
        };

        return amount.HasValue
            ? NormalizeMoney(amount.Value)
            : null;
    }

    public static decimal CalculateContractChangeAmount(
        PricingMethod pricingMethod,
        decimal previousValue,
        decimal newValue,
        decimal? unitPrice)
    {
        previousValue = NormalizeChangeValue(previousValue);
        newValue = NormalizeChangeValue(newValue);
        unitPrice = unitPrice.HasValue
            ? NormalizeMoney(unitPrice.Value)
            : null;

        var amount = pricingMethod switch
        {
            PricingMethod.LumpSum => newValue - previousValue,
            PricingMethod.UnitPrice or PricingMethod.TimeAndMaterial
                when unitPrice is > 0m =>
                (newValue - previousValue) * unitPrice.Value,
            PricingMethod.UnitPrice or PricingMethod.TimeAndMaterial =>
                throw new ArgumentOutOfRangeException(
                    nameof(unitPrice),
                    "UnitPrice must be positive for quantity-based ContractChange items."),
            PricingMethod.CostPlus => throw new InvalidOperationException(
                "CostPlus ContractChange items are disabled."),
            _ => throw new InvalidOperationException(
                $"ContractChange PricingMethod {pricingMethod} is not supported.")
        };

        return NormalizeMoney(amount);
    }

    public static decimal CalculatePercentageAmount(
        decimal amount,
        decimal percentage) =>
        NormalizeMoney(
            NormalizeMoney(amount) *
            NormalizePercentage(percentage) /
            100m);
}
