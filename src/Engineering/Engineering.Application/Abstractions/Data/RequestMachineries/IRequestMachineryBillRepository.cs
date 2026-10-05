using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryBillRepository : IBaseRepository<RequestMachineryBill>
{
    Task<long> BillNumberCreator(CT ct);

    Task<(List<RequestMachineryBill> Data, int RowCount)> GetFilteredAsync(
        List<long>? ids,
        long? requestMachineryId,
        long? costCenterId,
        long? projectId,
        List<long>? contractorIds,
        List<long>? projectOperationIds,
        long? machineriesGroupId,
        long? machineryId,
        DateTime? fromDate,
        DateTime? toDate,
        long? creatorId,
        int? requestNumber,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<RequestMachineryBill?> GetByIdAsync(long RequestMachineryBillId, CT ct);

    Task<bool?> IsDuplicateBill(long requestMachineryId, DateTime fromDate, DateTime toDate, CT ct);

    Task<List<string?>?> GetSupplierDrivers(long? supplierId, string? filterData, CT ct);

    Task<List<RequestMachineryBill>?> GetByIdsAsync(List<long> RequestMachineryBillIds, CT ct);
}
