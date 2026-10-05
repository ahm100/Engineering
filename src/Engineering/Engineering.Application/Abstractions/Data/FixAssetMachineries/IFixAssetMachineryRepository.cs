using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Abstractions.Data.FixAssetMachineries;

public interface IFixAssetMachineryRepository : IBaseRepository<FixAssetMachinery>
{
    Task<FixAssetMachinery?> GetById(
        long id,
        long? companyId,
        CT ct);
    Task<FixAssetMachinery?> GetFixAssetMachineryForDelete(
        long id,
        CT ct);
    Task<(List<FixAssetMachinery> Data, int RowCount)> GetsFixAssetMachineryByIds(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct);
    Task<(List<FixAssetMachinery> Data, int RowCount)> GetFixAssetMachineries(
        List<long>? ids,
        List<long>? machineryIds,
        List<long>? contractorIds,
        FixAssetMachineryType? type,
        string? numberPlates,
        DateTime? fromDate,
        DateTime? toDate,
        List<long>? driverIds,
        string? driverFilter,
        string? filterData,
        bool? isActive,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<long>?> GetFixAssetMachineryDriverIds(CT ct);
    Task<(List<FixAssetMachinery> Data, int RowCount)> GetActiveFixAssetMachineries(
        List<long>? machineryIds,
        FixAssetMachineryType? type,
        string? numberPlates,
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);
}