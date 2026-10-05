using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Abstractions.Data.FixAssetMachineries;

public interface IFixAssetMachineryRateRepository : IBaseRepository<FixAssetMachineryRate>
{
    Task<FixAssetMachineryRate?> GetById(long id, CT ct);

    Task<(List<FixAssetMachineryRate> Data, int RowCount)> GetsRateByFixAssetMachineryId(
        long fixAssetMachineryId,
        DateTime? fromDate,
        DateTime? toDate,
        int pageIndex,
        int pageSize,
        CT ct);
}