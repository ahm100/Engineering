using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedContractorGoodsSupply;

public record SetConfirmedContractorGoodsSupplyRequest : IHttpRequest
{
    public long Id { get; set; }
    public required List<GoodsSupplyManagementModelCommerce> Details { get; set; }
    public string? Description { get; set; }
    public bool RedFlag { get; set; } = false;
};
