namespace Engineering.Application.Services.RequestGoodsSupplies.Contracts.DeletePRequestGoodsSupplies;
public record DeleteProjectRequestGoodsSuppliesResponse
{
    public long Id { get; set; }
    public string RequestNumber { get; set; } = string.Empty;
    public bool IsDelete { get; set; }
}
