
namespace Engineering.Application.Services.ProjectServices.Models.GetsProjectServiceDetail;

public record GetsProjectServiceDetailResponse(
    List<GetsProjectServiceDetailModel> Data,
    int RowCount
    );


public record GetsProjectServiceDetailModel
{
    public long Id { get; set; }
    public long ServiceInfoId { get; set; }
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public string? ServiceInfoName { get; set; } = string.Empty;
    public long ServiceInfoMeasurId { get; set; }
    public string? ServiceInfoMeasur { get; set; } = string.Empty;

    public long? ContractorId { get; set; }
    public string? ContractorName { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;

    public long OperationInfoServiceId { get; set; }
    public long OperationInfoId { get; set; }
    public string? OperationInfoCode { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public long OperationInfoMeasurId { get; set; }
    public string? OperationInfoMeasur { get; set; } = string.Empty;

    public long ProjectServiceInfoId { get; set; }
    public string? ProjectServiceInfoCode { get; set; } = string.Empty;
    public string? ProjectServiceInfoName { get; set; } = string.Empty;
    public long ProjectServiceInfoMeasurId { get; set; }
    public string? ProjectServiceInfoMeasur { get; set; } = string.Empty;
    public decimal? ProjectServiceVolume { get; set; }
    public decimal? ProjectServiceDoneVolume { get; set; }
}
