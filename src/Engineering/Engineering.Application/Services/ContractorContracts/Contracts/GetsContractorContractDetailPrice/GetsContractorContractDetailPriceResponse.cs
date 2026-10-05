namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice;

public record GetsContractorContractDetailPriceResponse(
    List<GetsContractorContractDetailPriceModel> Data,
    int RowCount
    );

public record GetsContractorContractDetailPriceModel
{
    public long Id { get; set; }
    public long ContractorContractDetailId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Created { get; set; }
    public string? StartDateShamsi { get; set; }
    public string? EndDateShamsi { get; set; }
    public decimal? Price { get; set; }
    public bool? IsActive { get; set; }
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public long ServiceInfoUnitOfMeasurementId { get; set; }
    public string? ServiceInfoUnitOfMeasurement { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long OperationInfoUnitOfMeasurementId { get; set; }
    public string? OperationInfoUnitOfMeasurement { get; set; } = string.Empty;
}

