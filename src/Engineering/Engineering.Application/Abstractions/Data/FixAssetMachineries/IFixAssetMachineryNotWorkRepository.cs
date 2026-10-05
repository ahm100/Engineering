using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;
using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Abstractions.Data.FixAssetMachineries;

public interface IFixAssetMachineryNotWorkRepository : IBaseRepository<FixAssetMachineryNotWork>
{
    Task<FixAssetMachineryNotWork?> GetById(long id, CT ct);

    Task<FixAssetMachineryNotWork?> GetFixAssetMachineryNotWorkForDelete(long id, CT ct);

    Task<(List<FixAssetMachineryNotWork> Data, int RowCount)> GetsFixAssetMachineryNotWorkByIds(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<FixAssetMachineryNotWork> Data, int RowCount)> GetFixAssetMachineryNotWorks(
        List<long>? fixAssetMachineryIds,
        List<long>? machineryIds,
        FixAssetMachineryType? type,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsFixAssetMachineryNotWorkExcelExporterModel> Data, int RowCount)> GetsFixAssetMachineryNotWorkForExcel(
        List<long>? ids,
        List<long>? notWorkIds,
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
        int pageIndex,
        int pageSize,
        CT ct);
}