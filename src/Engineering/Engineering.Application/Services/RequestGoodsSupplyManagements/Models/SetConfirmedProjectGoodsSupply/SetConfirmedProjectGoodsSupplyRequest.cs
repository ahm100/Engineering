
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public record SetConfirmedProjectGoodsSupplyRequest : IHttpRequest
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public required List<SetConfirmedProjectGoodsSupplyDetail> Details { get; set; }
};
