
namespace Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoHistoryById;

public record GetsOperationInfoHistoryByIdResponse(
    List<GetsOperationInfoHistoryByIdModel?> Data,
    int RowCount);

public class GetsOperationInfoHistoryByIdModel
{
    public long Id { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string? OperationLatinName { get; set; }
    public int? Priority { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public decimal BasePrice { get; set; }
    public bool IsPriceList { get; set; }
    public bool HaveStandard { get; set; }
    public DateTime Created { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
}
