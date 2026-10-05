using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryRateById;

public record GetFixAssetMachineryRateByIdQuery(
    long Id
    ) : IQuery<FixAssetMachineryRate?>;