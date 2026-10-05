using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductHistoryById;

public record GetGoodsSupplyProductHistoryByIdResponse(
    List<GetGoodsSupplyProductHistoryByIdModel> Data,
    int? RowCount
    );


public record GetGoodsSupplyProductHistoryByIdModel
{
    public string? Created { get; set; }
    public GoodsSupplyDetailStatus Status { get; set; }
    public string? StatusDescription => Status.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
