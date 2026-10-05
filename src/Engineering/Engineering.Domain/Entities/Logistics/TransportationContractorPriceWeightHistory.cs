using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Logistics;

public class TransportationContractorPriceWeightHistory : AuditableEntity<TransportationContractorPriceWeightHistory>
{
    [Description(TransportationContractorPriceWeightCmts.TransportationContractorPriceWeight)]
    [ForeignKey("TransportationContractorPriceWeight")]
    public long TransportationContractorPriceWeightId { get; set; }
    public TransportationContractorPriceWeight TransportationContractorPriceWeight { get; set; }

    [Description(TransportationContractorPriceWeightCmts.UntilWeight)]
    public decimal UntilWeight { get; set; }

    [Description(TransportationContractorPriceWeightCmts.IsFixed)]
    public bool IsFixed { get; set; }

    [Description(TransportationContractorPriceWeightCmts.Price)]
    public decimal Price { get; set; }

    public ViewThirdParty Creator { get; set; }

    public TransportationContractorPriceWeightHistory(
        TransportationContractorPriceWeight priceWeight,
        decimal untilWeight,
        bool isFixed,
        decimal price) : this()
    {
        SetTransportationContractorPriceWeight(priceWeight);
        SetUntilWeight(untilWeight);
        SetPrice(price);
        SetIsFixed(isFixed);
    }

    public void SetTransportationContractorPriceWeight(TransportationContractorPriceWeight value)
    {
        TransportationContractorPriceWeight = Guard.Against.Null(value, nameof(value));
        TransportationContractorPriceWeightId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetUntilWeight(decimal untilWeight)
    {
        UntilWeight = Guard.Against.NegativeOrZero(untilWeight, nameof(untilWeight));
    }

    public void SetPrice(decimal price)
    {
        Price = Guard.Against.NegativeOrZero(price, nameof(price));
    }

    public void SetIsFixed(bool isFixed)
    {
        IsFixed = isFixed;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private TransportationContractorPriceWeightHistory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}

