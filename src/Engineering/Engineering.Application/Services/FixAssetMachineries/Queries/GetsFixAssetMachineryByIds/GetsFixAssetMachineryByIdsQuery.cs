using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetsFixAssetMachineryByIds;

public record GetsFixAssetMachineryByIdsQuery(
    List<long> ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<FixAssetMachinery?>>>;