namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsManagementHistoryById;

public record GetRequestGoodsManagementHistoryByIdResponse
{
    public long RequestGoodsSupplyManagementd { get; set; }
    public string? RequestNumber { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public decimal RequestedCount { get; set; }
    public List<GetRequestGoodsManagementHistoryByIdManagementModel>? Data { get; set; }
    public int? RowCount { get; set; }
}


