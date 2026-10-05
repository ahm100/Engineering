namespace Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;

public record GetProjectServiceByIdResponse
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
    public List<GetProjectServiceDetailModel>? ProjectServiceDetails { get; set; }
}

public record GetProjectServiceDetailModel
{
    public long Id { get; set; }
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public string? ServiceInfoName { get; set; } = string.Empty;
    public long ServiceInfoMeasurId { get; set; }
    public string? ServiceInfoMeasur { get; set; } = string.Empty;
    public long OperationInfoServiceId { get; set; }
    public long OperationInfoId { get; set; }
    public string? OperationInfoCode { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public long OperationInfoMeasurId { get; set; }
    public string? OperationInfoMeasur { get; set; } = string.Empty;
}
