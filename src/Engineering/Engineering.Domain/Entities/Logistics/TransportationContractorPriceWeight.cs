using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Logistics;

public class TransportationContractorPriceWeight : AuditableEntity<TransportationContractorPriceWeight>
{
    [Description(ShippingCostCmts.TransportationContractor)]
    [ForeignKey("TransportationContractor")]
    public long TransportationContractorId { get; set; }
    public TransportationContractor TransportationContractor { get; set; }

    [Description(TransportationContractorPriceWeightCmts.UntilWeight)]
    public decimal UntilWeight { get; set; }

    [Description(TransportationContractorPriceWeightCmts.IsFixed)]
    public bool IsFixed { get; set; }

    [Description(TransportationContractorPriceWeightCmts.Price)]
    public decimal Price { get; set; }

    public TransportationContractorPriceWeight(
        TransportationContractor transportationContractor,
        decimal untilWeight,
        bool isFixed,
        decimal price) : this()
    {
        SetTransportationContractor(transportationContractor);
        SetUntilWeight(untilWeight);
        SetPrice(price);
        SetIsFixed(isFixed);

        AddHistory();
    }

    public void Update(
        TransportationContractor transportationContractor,
        decimal untilWeight,
        bool isFixed,
        decimal price)
    {
        SetTransportationContractor(transportationContractor);
        SetUntilWeight(untilWeight);
        SetPrice(price);
        SetIsFixed(isFixed);

        AddHistory();
    }

    public void SetTransportationContractor(TransportationContractor value)
    {
        TransportationContractor = Guard.Against.Null(value, nameof(value));
        TransportationContractorId = Guard.Against.Null(value.Id, nameof(value.Id));
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

    public void AddHistory()
    {
        _priceWeightHistories.Add(new TransportationContractorPriceWeightHistory(
            this, UntilWeight, IsFixed, Price));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [Description(TransportationContractorCmts.PriceWeights)]
    private List<TransportationContractorPriceWeightHistory> _priceWeightHistories;
    public IReadOnlyList<TransportationContractorPriceWeightHistory> PriceWeightHistories => _priceWeightHistories;
    private TransportationContractorPriceWeight()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _priceWeightHistories = [];
    }
}

