using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetsRateByFixAssetMachineryId;

public record GetsRateByFixAssetMachineryIdQuery(
    long FixAssetMachineryId,
    DateTime? StartDate,
    DateTime? EndDate,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<FixAssetMachineryRate?>>>;