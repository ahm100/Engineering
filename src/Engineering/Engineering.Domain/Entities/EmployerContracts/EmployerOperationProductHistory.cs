namespace Engineering.Domain.Entities.EmployerContracts;

public class EmployerOperationProductHistory : AuditableEntity<EmployerOperationProductHistory, long>
{
    [Description(EContractCmts.ProductGroupId)]
    public long ProductGroupId { get; private set; }

    [Description(EContractCmts.ProductId)]
    public long? ProductId { get; private set; }

    [Description(EContractCmts.MinPrice)]
    public decimal MinPrice { get; private set; } = 0;

    [Description(EContractCmts.MaxPrice)]
    public decimal MaxPrice { get; private set; }

    [Description(EContractCmts.Count)]
    public int? Count { get; private set; }

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

    public EmployerOperationProduct EmployerOperationProduct { get; private set; }
    public long EmployerOperationProductId { get; private set; }

    public EmployerOperationProductHistory(
    EmployerOperationProduct product,
    long productGroupId,
    long? productId,
    decimal minPrice,
    decimal maxPrice,
    int? count,
    decimal tax,
    decimal taxPercent,
    decimal transportationCost,
    decimal transportationCostPercent,
    decimal profitCost,
    decimal profitCostPercent,
    decimal otherCost,
    decimal otherCostPercent,
    bool isStandard,
    string? description) : this()
    {
        SetEmployerOperationProduct(product);
        SetProductGroupId(productGroupId);
        SetProductId(productId);
        SetMinPrice(minPrice);
        SetMaxPrice(maxPrice);
        SetCount(count);
        SetTax(tax);
        SetTaxPercent(taxPercent);
        SetTransportationCost(transportationCost);
        SetTransportationCostPercent(transportationCostPercent);
        SetProfitCost(profitCost);
        SetProfitCostPercent(profitCostPercent);
        SetOtherCost(otherCost);
        SetOtherCostPercent(otherCostPercent);
        SetIsStandard(isStandard);
        SetDescription(description);
    }

    private void SetEmployerOperationProduct(EmployerOperationProduct value)
    {
        EmployerOperationProduct = Guard.Against.Null(value, nameof(value));
        EmployerOperationProductId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetProductGroupId(long value)
    {
        ProductGroupId = Guard.Against.Null(value, nameof(value));
    }

    private void SetMinPrice(decimal value)
    {
        MinPrice = Guard.Against.Null(value, nameof(value)); ;
    }

    private void SetMaxPrice(decimal value)
    {
        MaxPrice = Guard.Against.Null(value, nameof(value));
    }

    private void SetProductId(long? value)
    {
        ProductId = value; ;
    }

    private void SetCount(int? value)
    {
        Count = value;
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

    private void SetTax(decimal value)
    {
        Tax = Guard.Against.Null(value, nameof(value));
    }

    private void SetTaxPercent(decimal value)
    {
        TaxPercent = Guard.Against.Null(value, nameof(value));
    }

    private void SetTransportationCost(decimal value)
    {
        TransportationCost = Guard.Against.Null(value, nameof(value));
    }

    private void SetTransportationCostPercent(decimal value)
    {
        TransportationCostPercent = Guard.Against.Null(value, nameof(value));
    }

    private void SetIsStandard(bool value)
    {
        IsStandard = Guard.Against.Null(value, nameof(value));
    }

    private void SetDescription(string? value)
    {
        Description = value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerOperationProductHistory()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}