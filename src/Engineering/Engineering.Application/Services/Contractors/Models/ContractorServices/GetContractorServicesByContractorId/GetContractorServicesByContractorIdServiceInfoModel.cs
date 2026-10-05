namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorServicesByContractorId;

public record GetContractorServicesByContractorIdServiceInfoModel
{
    public long Id { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public string ServiceInfoCode { get; set; } = string.Empty;
    public long UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
