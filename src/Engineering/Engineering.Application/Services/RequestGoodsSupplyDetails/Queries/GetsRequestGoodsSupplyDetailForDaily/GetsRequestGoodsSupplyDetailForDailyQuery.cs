using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetailForDaily;

public record GetsRequestGoodsSupplyDetailForDailyQuery(
    long ProjectOperationDetailId
    ) : IQuery<DataResult<List<RequestGoodsSupplyDetail>>>;
