using Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyDetail;

public record CreateRequestGoodsSupplyDetailCommand(
    RequestGoodsSupply RequestGoodsSupply,
    ConsumableVolumeProduct? ConsumableVolumeProduct,
    ProjectProduct? ProjectProduct,
    RequestGoodsSupplyProduct? RequestGoodsSupplyProduct,
    CreateRequestGoodsSupplyDetailModel Detail,
    decimal? TotalPrice,
    decimal? DiscountedPrice,
    decimal? FinalPrice
    ) : ICommand<RequestGoodsSupplyDetail>;

public record CreateProjectRequestGoodsSupplyDetailCommand(
    RequestGoodsSupply RequestGoodsSupply,
    ConsumableVolumeProduct? ConsumableVolumeProduct,
    ProjectProduct? ProjectProduct,
    RequestGoodsSupplyProduct? RequestGoodsSupplyProduct,
    CreateProjectRequestGoodsSupplyDetailModel Detail,
    decimal? TotalPrice,
    decimal? DiscountedPrice,
    decimal? FinalPrice
    ) : ICommand<RequestGoodsSupplyDetail>;

