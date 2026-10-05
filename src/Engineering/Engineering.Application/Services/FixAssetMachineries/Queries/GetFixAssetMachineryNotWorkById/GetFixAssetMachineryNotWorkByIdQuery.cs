using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryNotWorkById;

public record GetFixAssetMachineryNotWorkByIdQuery(
    long Id
    ) : IQuery<FixAssetMachineryNotWork?>;