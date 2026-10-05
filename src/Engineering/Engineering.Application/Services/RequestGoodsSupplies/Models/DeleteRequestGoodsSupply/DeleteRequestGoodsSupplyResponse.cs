namespace Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRequestGoodsSupply;

public class DeleteRequestGoodsSupplyResponse
{
    public long Id { get; set; }
    public string RequestNumber { get; set; } = string.Empty;
    public bool IsDelete { get; set; }

}
