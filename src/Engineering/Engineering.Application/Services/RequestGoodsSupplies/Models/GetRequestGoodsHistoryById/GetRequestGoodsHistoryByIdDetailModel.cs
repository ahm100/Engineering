using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsHistoryById;

public record GetRequestGoodsHistoryByIdDetailModel
{
    public string? Created { get; set; }
    public GoodsSupplyStatus Status { get; set; }
    public string? StatusDescription { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
