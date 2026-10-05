
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetSingleConfirmedProjectGoodsSupply;

public record SetSingleConfirmedProjectGoodsSupplyRequest : IHttpRequest
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public required SetSingleConfirmedProjectGoodsSupplyDetail Detail { get; set; }
};
