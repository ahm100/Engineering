using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;

public record GetDetailByRGSIdResponse(List<GetDetailByRGSIdModel> Data,
    int RowCount);

public class GetDetailByRGSIdModel
{
    public long Id { get; set; }
    public long? ReferenceId { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string? ReferenceEn { get; set; } = string.Empty;
    public string ReferenceCode { get; set; } = string.Empty;
    public string? Measure { get; set; } = string.Empty;
    public string? MeasureEn { get; set; } = string.Empty;
    public SupplyType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public string? CostCenterEnName { get; set; }
    public DateTime? DeliveryDeadLine { get; set; }
    public string? DeliveryDeadLineShamsi => DeliveryDeadLine.ToShamsi();
    public decimal RequestedCount { get; set; }
    public long RequestGoodsSupplyTypeId { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public string? CreatorEnName { get; set; }
    public List<string>? Urls { get; set; }
}