namespace Engineering.Domain.Entities.EmployerContracts;

public class EmployerOperationServiceHistory : AuditableEntity<EmployerOperationServiceHistory, long>
{
    [Description(EContractCmts.MinPrice)]
    public decimal MinPrice { get; private set; } = 0;

    [Description(EContractCmts.MaxPrice)]
    public decimal MaxPrice { get; private set; }

    [Description(EContractCmts.Tax)]
    public decimal Tax { get; private set; }

    [Description(EContractCmts.TaxPercent)]
    public decimal TaxPercent { get; private set; }

    [Description(EContractCmts.TransportationCost)]
    public decimal TransportationCost { get; private set; }

    [Description(EContractCmts.TransportationCostPercent)]
    public decimal TransportationCostPercent { get; private set; }

    [Description(EContractCmts.ProfitCost)]
    public decimal ProfitCost { get; private set; }

    [Description(EContractCmts.ProfitCostPercent)]
    public decimal ProfitCostPercent { get; private set; }

    [Description(EContractCmts.OtherCost)]
    public decimal OtherCost { get; private set; }

    [Description(EContractCmts.OtherCostPercent)]
    public decimal OtherCostPercent { get; private set; }

    [Description(EContractCmts.IsStandard)]
    public bool IsStandard { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    public EmployerOperationService EmployerOperationService { get; private set; }
    public long EmployerOperationServiceId { get; private set; }

    public EmployerOperationServiceHistory(
        EmployerOperationService employerOperationService,
        decimal minPrice,
        decimal maxPrice,
        decimal tax,
        decimal taxPercent,
        decimal transportationCost,
        decimal transportationCostPercent,
        decimal profitCost,
        decimal profitCostPercent,
        decimal otherCost,
        decimal otherCostPercent,
        string? description) : this()
    {
        SetEmployerOperationService(employerOperationService);
        SetMinPrice(minPrice);
        SetMaxPrice(maxPrice);
        SetTax(tax);
        SetTaxPercent(taxPercent);
        SetTransportationCost(transportationCost);
        SetTransportationCostPercent(transportationCostPercent);
        SetProfitCost(profitCost);
        SetProfitCostPercent(profitCostPercent);
        SetOtherCost(otherCost);
        SetOtherCostPercent(otherCostPercent);
        SetDescription(description);
    }

    private void SetEmployerOperationService(EmployerOperationService value)
    {
        EmployerOperationService = Guard.Against.Null(value, nameof(value));
        EmployerOperationServiceId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetMinPrice(decimal value)
    {
        MinPrice = Guard.Against.Null(value, nameof(value)); ;
    }

    private void SetMaxPrice(decimal value)
    {
        MaxPrice = Guard.Against.Null(value, nameof(value));
    }

    private void SetTax(decimal value)
    {
        Tax = Guard.Against.Null(value, nameof(value));
    }

    private void SetTaxPercent(decimal value)
    {
        TaxPercent = Guard.Against.Null(value, nameof(value));
    }

    private void SetProfitCost(decimal value)
    {
        ProfitCost = Guard.Against.Null(value, nameof(value));
    }

    private void SetProfitCostPercent(decimal value)
    {
        ProfitCostPercent = Guard.Against.Null(value, nameof(value));
    }

    private void SetOtherCost(decimal value)
    {
        OtherCost = Guard.Against.Null(value, nameof(value));
    }

    private void SetOtherCostPercent(decimal value)
    {
        OtherCostPercent = Guard.Against.Null(value, nameof(value));
    }

    private void SetTransportationCost(decimal value)
    {
        TransportationCost = Guard.Against.Null(value, nameof(value));
    }

    private void SetTransportationCostPercent(decimal value)
    {
        TransportationCostPercent = Guard.Against.Null(value, nameof(value));
    }

    private void SetDescription(string? value)
    {
        Description = value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerOperationServiceHistory()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}