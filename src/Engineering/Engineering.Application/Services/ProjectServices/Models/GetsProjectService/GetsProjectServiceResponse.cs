
namespace Engineering.Application.Services.ProjectServices.Models.GetsProjectService;

public record GetsProjectServiceResponse(
    List<GetsProjectServiceModel> Data,
    int RowCount
    );


public record GetsProjectServiceModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterCode { get; set; } = string.Empty;
    public string? CostCenterName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectCode { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public string? ServiceInfoName { get; set; } = string.Empty;
    public long? ServiceInfoMeasurId { get; set; }
    public string? ServiceInfoMeasur { get; set; } = string.Empty;
    public long ContractorId { get; set; }
    public string? ContractorName { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;
    public decimal Volume { get; set; }
    public decimal DoneVolume { get; set; }
    public decimal RemainderVolume { get; set; }
    public bool IsActive { get; set; }
    public DateTime Created { get; set; }
}
