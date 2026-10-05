using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.EmployerOperationDetail)]
public class EmployerOperationService : AuditableEntity<EmployerOperationService, long>
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

    [Description(EContractCmts.EmployerOperation)]
    public EmployerOperation EmployerOperation { get; private set; }
    public long EmployerOperationId { get; private set; }

    [Description(EContractCmts.OperationInfoSerivce)]
    public OperationInfoService? OperationInfoService { get; private set; }
    public long? OperationInfoServiceId { get; private set; }

    [Description(EContractCmts.ServiceInfo)]
    public ServiceInfo? ServiceInfo { get; private set; }
    public long? ServiceInfoId { get; private set; }

    public EmployerOperationService(
        EmployerOperation operation,
        OperationInfoService? operationInfoService,
        ServiceInfo? serviceInfo,
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
        bool isStandard,
        string? description) : this()
    {
        SetEmployerOperation(operation);
        SetOperationInfoService(operationInfoService);
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
        SetServiceInfo(serviceInfo);
        SetIsStandard(isStandard);
        AddHistory();
    }

    public EmployerOperationService Create(
        EmployerOperation employerOperation,
        OperationInfoService operationInfoService,
        ServiceInfo? serviceInfo,
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
        bool isStandard,
        string? description)
    {
        return new EmployerOperationService(
            employerOperation,
            operationInfoService,
            serviceInfo,
            minPrice,
            maxPrice,
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
        decimal tax,
        decimal taxPercent,
        decimal transportationCost,
        decimal transportationCostPercent,
        decimal profitCost,
        decimal profitCostPercent,
        decimal otherCost,
        decimal otherCostPercent,
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
        SetDescription(description);
        AddHistory();
    }

    public void AddHistory()
    {
        _employerOperationServiceHistories.Add(new EmployerOperationServiceHistory(
            this,
            MinPrice,
            MaxPrice,
            Tax,
            TaxPercent,
            TransportationCost,
            TransportationCostPercent,
            ProfitCost,
            ProfitCostPercent,
            OtherCost,
            OtherCostPercent,
            Description));
    }

    private void SetEmployerOperation(EmployerOperation value)
    {
        EmployerOperation = Guard.Against.Null(value, nameof(value));
        EmployerOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    private void SetOperationInfoService(OperationInfoService? value)
    {
        OperationInfoService = value;
        OperationInfoServiceId = value?.Id;
    }

    private void SetServiceInfo(ServiceInfo? value)
    {
        ServiceInfo = value;
        ServiceInfoId = value?.Id;
    }

    private void SetIsStandard(bool value)
    {
        IsStandard = Guard.Against.Null(value, nameof(value));
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
    [Description(EContractCmts.EmployerOperationProductHistory)]
    private List<EmployerOperationServiceHistory> _employerOperationServiceHistories;
    public IReadOnlyList<EmployerOperationServiceHistory> EmployerOperationServiceHistories => _employerOperationServiceHistories;
    private EmployerOperationService()
    {
        _employerOperationServiceHistories = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}