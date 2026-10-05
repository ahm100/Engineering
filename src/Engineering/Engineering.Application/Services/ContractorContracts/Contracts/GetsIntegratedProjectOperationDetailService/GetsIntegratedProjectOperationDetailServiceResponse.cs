namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsIntegratedProjectOperationDetailService;

public record GetsIntegratedProjectOperationDetailServiceResponse(
    List<GetsIntegratedProjectOperationDetailServiceModel> Data,
    int RowCount
    );

public record GetsIntegratedProjectOperationDetailServiceModel
{
    public string OperationInfoNames { get; set; } = string.Empty;
    public string OperationInfoCodes { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; }
    public long? ProjectServiceId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public decimal ServiceInfoVolume { get; set; }
    public string? ServiceInfoUnitOfMeasurement { get; set; } = string.Empty;
    public string? OperationLocationPrivateNames { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public bool CollectiveService { get; set; }
    public decimal FinalAmount { get; set; }
    public bool HasExperts { get; set; }
}
