namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;

public record GetsPartialProjectOperationDetailServiceResponse(
    List<GetsPartialProjectOperationDetailServiceModel> Data
    );

public record GetsPartialProjectOperationDetailServiceModel
{
    public long ProjectOperationDetailServiceId { get; set; }
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public decimal ServiceInfoVolume { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public string? ServiceInfoUnitOfMeasurement { get; set; } = string.Empty;
    public string? OperationLocationPrivateName { get; set; } = string.Empty;
    public string? OperationLocationPrivateCode { get; set; } = string.Empty;
    public string? OperationLocationPublicName { get; set; } = string.Empty;
    public string? OperationLocationPublicCode { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
