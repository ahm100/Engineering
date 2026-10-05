namespace Engineering.Application.Services.ProjectServices.Models.GetsServiceInfoByProjectId;

public record GetsServiceInfoByProjectIdResponse
{
    public List<GetsServiceInfoByProjectIdModel>? Data { get; set; }
    public int RowCount { get; set; }
}
public record GetsServiceInfoByProjectIdModel
{
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public string? ServiceInfoTitle { get; set; } = string.Empty;
    public long? ServiceInfoMeasurId { get; set; }
    public string? ServiceInfoMeasur { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime Created { get; set; }
}