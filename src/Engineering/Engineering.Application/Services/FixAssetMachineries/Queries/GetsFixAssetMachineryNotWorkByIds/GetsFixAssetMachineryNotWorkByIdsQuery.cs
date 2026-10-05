using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetsFixAssetMachineryNotWorkByIds;

public record GetsFixAssetMachineryNotWorkByIdsQuery(
    List<long> ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<FixAssetMachineryNotWork?>>>;