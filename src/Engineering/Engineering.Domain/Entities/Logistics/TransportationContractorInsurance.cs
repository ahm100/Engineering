using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Logistics;

public class TransportationContractorInsurance : AuditableEntity<TransportationContractorInsurance>
{
    [Description(ShippingCostCmts.TransportationContractor)]
    [ForeignKey("TransportationContractor")]
    public long TransportationContractorId { get; set; }
    public TransportationContractor TransportationContractor { get; set; }

    [Description(TransportationContractorInsuranceCmts.MinProductPrice)]
    public decimal MinProductPrice { get; set; }

    [Description(TransportationContractorInsuranceCmts.MaxProductPrice)]
    public decimal MaxProductPrice { get; set; }

    [Description(TransportationContractorInsuranceCmts.FixedPrice)]
    public decimal FixedPrice { get; set; }

    [Description(TransportationContractorInsuranceCmts.Multiplication)]
    public decimal? Multiplication { get; set; }

    [Description(TransportationContractorInsuranceCmts.Division)]
    public decimal? Division { get; set; }

    [Description(TransportationContractorInsuranceCmts.Subtraction)]
    public decimal? Subtraction { get; set; }

    [Description(TransportationContractorInsuranceCmts.Addition)]
    public decimal? Addition { get; set; }

    public TransportationContractorInsurance(
        TransportationContractor transportationContractor,
        decimal minProductPrice,
        decimal maxProductPrice,
        decimal fixedPrice,
        decimal? multiplication,
        decimal? division,
        decimal? subtraction,
        decimal? addition
        ) : this()
    {
        SetTransportationContractor(transportationContractor);
        SetMinProductPrice(minProductPrice);
        SetMaxProductPrice(maxProductPrice);
        SetFixedPrice(fixedPrice);
        SetMultiplication(multiplication);
        SetDivision(division);
        SetSubtraction(subtraction);
        SetAddition(addition);
    }

    public void Update(
        decimal minProductPrice,
        decimal maxProductPrice,
        decimal fixedPrice,
        decimal? multiplication,
        decimal? division,
        decimal? subtraction,
        decimal? addition)
    {
        SetMinProductPrice(minProductPrice);
        SetMaxProductPrice(maxProductPrice);
        SetFixedPrice(fixedPrice);
        SetMultiplication(multiplication);
        SetDivision(division);
        SetSubtraction(subtraction);
        SetAddition(addition);
    }

    public void SetTransportationContractor(TransportationContractor value)
    {
        TransportationContractor = Guard.Against.Null(value, nameof(value));
        TransportationContractorId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetMinProductPrice(decimal value)
    {
        MinProductPrice = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public void SetMaxProductPrice(decimal value)
    {
        MaxProductPrice = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public void SetFixedPrice(decimal value)
    {
        FixedPrice = Guard.Against.NegativeOrZero(value, nameof(value));
    }

    public void SetMultiplication(decimal? value)
    {
        Multiplication = value;
    }

    public void SetDivision(decimal? value)
    {
        Division = value;
    }

    public void SetSubtraction(decimal? value)
    {
        Subtraction = value;
    }

    public void SetAddition(decimal? value)
    {
        Addition = value;
    }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private TransportationContractorInsurance()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}

