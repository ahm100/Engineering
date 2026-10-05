using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceItems;

public record GetReferenceItemsResponse(
    List<GetReferenceItemsModel> Data,
    int RowCount);

public class GetReferenceItemsModel
{
    public long Id { get; set; }
    public SupplyType SupplyType { get; set; }
    public string SupplyTypeDescription => SupplyType.GetEnumDescription();
    public string? Name { get; set; }
    public string? NameEn { get; set; }
    public string? Code { get; set; }
    public string? TechnicalCode { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedShamsi => Created.ToShamsi();
}