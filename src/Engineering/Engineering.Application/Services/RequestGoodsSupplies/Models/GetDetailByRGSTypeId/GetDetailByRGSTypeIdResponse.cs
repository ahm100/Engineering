namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;

public record GetDetailByRGSTypeIdResponse
(
    List<GetDetailByRGSTypeIdModel> Data,
    int RowCount
);
public class GetDetailByRGSTypeIdModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public DateTime? DeliveryDeadLine { get; set; }
    public string? DeliveryDeadLineShamsi => DeliveryDeadLine.ToShamsi();
    public decimal RequestedCount { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public List<string>? Urls { get; set; }
}