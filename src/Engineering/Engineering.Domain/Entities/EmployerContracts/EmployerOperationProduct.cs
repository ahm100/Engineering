namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerOperationDetail)]
public class EmployerOperationProduct : AuditableEntity<EmployerOperationProduct, long>
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

    [Description(EContractCmts.EmployerOperation)]
    public EmployerOperation EmployerOperation { get; private set; }
    public long EmployerOperationId { get; private set; }

    [Description(EContractCmts.ConsumptionStandardProduct)]
    public ConsumptionStandardProduct? ConsumptionStandardProduct { get; private set; }
    public long? ConsumptionStandardProductId { get; private set; }

    public EmployerOperationProduct(
        EmployerOperation operation,
        ConsumptionStandardProduct? product,
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
        SetEmployerOperation(operation);
        SetConsumptionStandardProduct(product);
        SetProductGroupId(productGroupId);
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
        SetIsStandard(isStandard);
        SetDescription(description);
        SetProductId(productId);
        SetCount(count);
    }

    public EmployerOperationProduct Create(
        EmployerOperation employerOperation,
        ConsumptionStandardProduct consumptionStandardProduct,
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
        string? description)
    {
        return new EmployerOperationProduct(
            employerOperation,
            consumptionStandardProduct,
            productGroupId,
            productId,
            minPrice,
            maxPrice,
            count,
            tax,
            taxPercent,
            transportationCost,
            transportationCostPercent,
            profitCost,
            profitCostPercent,
            otherCost,
            otherCostPercent,
            isStandard,
            description);
    }

    public void Update(
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
        bool? isStandard,
        string? description)
    {
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
        SetIsStandard(isStandard ?? IsStandard);
        SetDescription(description);
        SetCount(count);
    }

    public void AddHistory()
    {
        _employerOperationProductHistories.Add(new EmployerOperationProductHistory(
            this,
            ProductGroupId,
            ProductId,
            MinPrice,
            MaxPrice,
            Count,
            Tax,
            TaxPercent,
            TransportationCost,
            TransportationCostPercent,
            ProfitCost,
            ProfitCostPercent,
            OtherCost,
            OtherCostPercent,
            IsStandard,
            Description));
    }


    private void SetEmployerOperation(EmployerOperation value)
    {
        EmployerOperation = Guard.Against.Null(value, nameof(value));
        EmployerOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetConsumptionStandardProduct(ConsumptionStandardProduct? value)
    {
        ConsumptionStandardProduct = value;
        ConsumptionStandardProductId = value?.Id;
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

    [Description(EContractCmts.EmployerOperationProductHistory)]
    private List<EmployerOperationProductHistory> _employerOperationProductHistories;
    public IReadOnlyList<EmployerOperationProductHistory> EmployerOperationProductHistories => _employerOperationProductHistories;
    private EmployerOperationProduct()
    {
        _employerOperationProductHistories = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}