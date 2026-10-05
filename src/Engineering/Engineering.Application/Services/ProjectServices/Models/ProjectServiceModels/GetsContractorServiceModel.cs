namespace Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;

public record GetsContractorServiceModel
{
    public long Id { get; set; }
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public string? ServiceInfoTitle { get; set; } = string.Empty;
    public long? ServiceInfoMeasurId { get; set; }
    public string? ServiceInfoMeasur { get; set; } = string.Empty;
    public long ContractorId { get; set; }
    public string? ContractorName { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public decimal Volume { get; set; }
    public decimal DoneVolume { get; set; }
    public decimal RemainderVolume { get; set; }
    public DateTime Created { get; set; }
}


