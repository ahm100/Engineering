
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedGoodsSupplyProduct;

public record SetConfirmedGoodsSupplyProductRequest : IHttpRequest
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public required SetConfirmedGoodsSupplyProductModel Detail { get; set; }
};

public record SetConfirmedGoodsSupplyProductModelRequest
{
    public long Id { get; set; }
    public RequestGoodsSupplyProduct? ProductEntity { get; set; }
    public string? Description { get; set; }
    public required SetConfirmedGoodsSupplyProductModel Detail { get; set; }
};
