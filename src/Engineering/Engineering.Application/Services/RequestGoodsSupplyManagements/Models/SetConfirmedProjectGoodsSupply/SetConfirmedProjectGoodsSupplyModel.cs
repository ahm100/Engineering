using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public record SetConfirmedProjectGoodsSupplyModel(
    RequestGoodsSupplyProduct Detail,
    long ProductId,
    long? AlternateId,
    long MeasureUnitId
    );
