using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.SetOperationInfoSeasonToGoodsSupply;

public record SetOperationInfoSeasonToGoodsSupplyCommand(
    RequestGoodsSupply GoodsSupply,
    OperationInfoSeason OperationInfoSeason
    ) : ICommand<RequestGoodsSupply>;


