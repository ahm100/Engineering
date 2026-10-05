
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.SetOperationInfoSeasonToGoodsSupply;

public record SetOperationInfoSeasonToGoodsSupplyRequest(
    long Id,
    long SeasonId
    ) : IHttpRequest;
