
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedServiceContract;

public record GetsRequestedServiceContractModel
{
    //public long ProjectOperationDetailServiceId { get; set; }
    public long ServiceInfoId { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public string ServiceInfoCode { get; set; } = string.Empty;
    public decimal ServiceInfoVolume { get; set; }
    public string OperationLocationPrivateNames { get; set; } = string.Empty;
    public long? UnitOfMeasurementId { get; set; }
    public string? ServiceInfoUnitOfMeasurement { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public string OperationInfoNames { get; set; } = string.Empty;
    public string OperationInfoCodes { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public List<long> ProjectOperationIds { get; set; } = new();
}
