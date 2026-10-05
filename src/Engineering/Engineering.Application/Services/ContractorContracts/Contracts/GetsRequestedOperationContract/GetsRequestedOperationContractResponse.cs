
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedOperationContract;

public record GetsRequestedOperationContractResponse
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public List<GetsRequestedOperationContractModel> Details { get; set; } = new();
    public int RowCount => Details.Count;
}

public record GetsRequestedOperationContractModel
{
    public long ProjectOperationDetailServiceId { get; set; }
    public long ProjectOperationId { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoCode { get; set; } = string.Empty;
    public string OperationInfoName { get; set; } = string.Empty;
    public long OperationWorkload { get; set; }
    public long? UnitOfMeasurementId { get; set; }
    public string? OperationUnitOfMeasurement { get; set; } = string.Empty;
}
