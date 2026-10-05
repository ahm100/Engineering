
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Extensions.Calculators;

public static class Calculator
{
    // محاسبه درصد تخفیف
    public static decimal CalculateDiscountPercentage(decimal discount, decimal totalAmount)
    {
        decimal discountPercentage = (discount / totalAmount) * 100;
        return discountPercentage;
    }

    // محاسبه مبلغ تخفیف
    public static decimal CalculateDiscountAmount(decimal discountPercentage, decimal totalAmount)
    {
        decimal discountAmount = (discountPercentage / 100) * totalAmount;
        return discountAmount;
    }

    public static decimal FinalAmount(decimal length, decimal width, decimal height, decimal weight, decimal number)
    {
        return length * width * height * weight * number;
    }

    public static float? RoundingDTFII(decimal? value)
    {
        if (value is null || value == 0)
            return 0;
        else
            return (float)Math.Round(value.Value, 2);
    }

    public static decimal? RoundingDecimalDTFII(decimal? value)
    {
        if (value is null || value == 0)
            return 0;
        else
            return Math.Round(value.Value, 2);
    }

    public static float? RoundingDTFII(float? value)
    {
        if (value is null || value == 0)
            return 0;
        else
            return (float)Math.Round(value.Value, 2);
    }

    public static float? RoundingDTFV(decimal? value)
    {
        if (value is null || value == 0)
            return 0;
        else
            return (float)Math.Round(value.Value, 5);
    }

    public static decimal? RoundingDecimalDTFV(decimal? value)
    {
        if (value is null || value == 0)
            return 0;
        else
            return Math.Round(value.Value, 5);
    }

    public static float? RoundingDTFV(float? value)
    {
        if (value is null || value == 0)
            return 0;
        else
            return (float)Math.Round(value.Value, 5);
    }

    public static float? RoundingDTFVI(decimal? value)
    {
        if (value is null || value == 0)
            return 0;
        else
            return (float)Math.Round(value.Value, 6);
    }

    public static float? RoundingDTFVI(float? value)
    {
        if (value is null || value == 0)
            return 0;
        else
            return (float)Math.Round(value.Value, 6);
    }

    public static decimal? CalculateTransportPriceWeight(TransportationContractorPriceWeight[] maps, decimal weight)
    {
        decimal sum = 0m;
        decimal remaining = Math.Ceiling(weight);
        var lastNotFixed = maps.Where(x => !x.IsFixed)
            .MaxBy(x => x.UntilWeight);

        var lastFixed = maps.Where(x => x.IsFixed)
            .Select(x => x.Price).FirstOrDefault();

        if (lastNotFixed!.UntilWeight >= remaining)
        {
            sum += maps.FirstOrDefault(z => z.UntilWeight == remaining || z.UntilWeight >= remaining)?.Price ?? 0;
        }
        else if (lastNotFixed.UntilWeight < remaining)
        {
            var untilWeight = remaining - lastNotFixed.UntilWeight;
            var untilPrice = lastFixed * untilWeight;

            sum += (untilPrice + lastNotFixed.Price);
        }

        return sum;
    }
}
