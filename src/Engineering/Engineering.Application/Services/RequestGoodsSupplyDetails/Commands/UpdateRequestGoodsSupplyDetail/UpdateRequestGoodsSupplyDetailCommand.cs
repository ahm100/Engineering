using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyDetail;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.UpdateRequestGoodsSupplyDetail;

public record UpdateRequestGoodsSupplyDetailCommand(
    RequestGoodsSupplyDetail Entity,
    ConsumableVolumeProduct VolumeProduct,
    RequestGoodsSupplyProduct? Product,
    UpdateRequestGoodsSupplyDetailRequest Detail,
    decimal? TotalPrice,
    decimal? DiscountedPrice,
    decimal? FinalPrice
    ) : ICommand<RequestGoodsSupplyDetail>;