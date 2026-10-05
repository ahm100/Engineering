using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Abstractions.Data.FixAssetMachineries;

public interface IMachineryReservationRepository : IBaseRepository<MachineryReservation>
{
    Task<MachineryReservation?> GetById(long id, long? companyId, CT ct);

    Task<MachineryReservation?> GetMachineryReservationForDelete(long id, CT ct);

    Task<(List<MachineryReservation> Data, int RowCount)> GetsMachineryReservationByIds(List<long> ids, int pageIndex, int pageSize, CT ct);

    Task<(List<MachineryReservation> Data, int RowCount)> GetMachineryReservations(
        List<long>? ids,
        List<long>? machineryIds,
        List<long>? fixAssetMachineryIds,
        List<long>? requestMachineryIds,
        MachineryReservationUnit? unit,
        MachineryReservationStatus? status,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);
}