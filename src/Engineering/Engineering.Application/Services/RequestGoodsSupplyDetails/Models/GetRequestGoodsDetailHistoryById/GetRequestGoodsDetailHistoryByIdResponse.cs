namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsDetailHistoryById;

public record GetRequestGoodsDetailHistoryByIdResponse
{
    public long RequestGoodsSupplyDetaild { get; set; }
    public string? RequestNumber { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public long ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public decimal RequestCount { get; set; }
    public List<GetRequestGoodsDetailHistoryByIdDetailModel>? Data { get; set; }
    public int? RowCount { get; set; }
}


