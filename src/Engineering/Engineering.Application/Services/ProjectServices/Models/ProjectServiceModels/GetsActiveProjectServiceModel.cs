namespace Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;

public record GetsActiveProjectServiceModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterCode { get; set; } = string.Empty;
    public string? CostCenterTitle { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectCode { get; set; } = string.Empty;
    public string? ProjectTitle { get; set; } = string.Empty;
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public string? ServiceInfoTitle { get; set; } = string.Empty;
    public long? ServiceInfoMeasurId { get; set; }
    public string? ServiceInfoMeasur { get; set; } = string.Empty;
    public long ContractorId { get; set; }
    public string? ContractorName { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;
    public decimal Volume { get; set; }
    public decimal DoneVolume { get; set; }
    public decimal RemainderVolume { get; set; }
    public DateTime Created { get; set; }
}
