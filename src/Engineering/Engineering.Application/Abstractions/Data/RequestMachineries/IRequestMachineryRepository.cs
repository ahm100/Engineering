using Engineering.Application.Services.RequestMachineryBills.Models.GetSupplierDrivers;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryRepository : IBaseRepository<RequestMachinery>
{
    Task<long> RequestNumberCreator(long? companyId, CT ct);

    Task<(List<RequestMachinery> Data, int RowCount)> GetFiltered(
        List<long>? ids,
        long? costCenterId,
        long? projectId,
        List<long>? contractorIds,
        List<long>? projectOperationIds,
        List<long>? operationInfoIds,
        long? machineriesGroupId,
        long? machineryId,
        RequestMachineryStatus? status,
        RequestMachineryPaymentType? paymentType,
        DateTime? fromDate,
        DateTime? toDate,
        DateTime? confirmedFromDate,
        DateTime? confirmedToDate,
        long? creatorId,
        long? operatorAppoinmentUserId,
        int? requestNumber,
        string? driverName,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<RequestMachinery> Data, int RowCount)> GetOnProjectMachineryReports(
          List<long>? ids,
          List<long>? costCenterIds,
          List<long>? projectIds,
          long? contractorId,
          List<long>? projectOperationIds,
          List<long>? projectOperationDetailIds,
          List<long>? machineryIds,
          RequestMachineryUnit? unit,
          List<RequestMachineryStatus>? statuses,
          DateTime? startDate,
          DateTime? endDate,
          DateTime? confirmedFromDate,
          DateTime? confirmedToDate,
          string? filterData,
          bool? ownCompany,
          string[]? orderBy,
          long? companyId,
          int pageIndex,
          int pageSize,
          CT ct);

    Task<(List<RequestMachinery> Data, int RowCount)> GetSendManagerMachineryReports(
          List<long>? ids,
          List<long>? costCenterIds,
          List<long>? projectIds,
          long? contractorId,
          List<long>? projectOperationIds,
          List<long>? projectOperationDetailIds,
          List<long>? machineryIds,
          RequestMachineryUnit? unit,
          List<RequestMachineryStatus>? statuses,
          DateTime? startDate,
          DateTime? endDate,
          DateTime? confirmedFromDate,
          DateTime? confirmedToDate,
          string? filterData,
          bool? ownCompany,
          string[]? orderBy,
          long? companyId,
          int pageIndex,
          int pageSize,
          CT ct);

    Task<(List<RequestMachinery> Data, int RowCount)> GetManagerConfirmedMachineryReports(
       List<long>? ids,
       List<long>? costCenterIds,
       List<long>? projectIds,
       long? contractorId,
       List<long>? projectOperationIds,
       List<long>? projectOperationDetailIds,
       List<long>? machineryIds,
       RequestMachineryUnit? unit,
       List<RequestMachineryStatus>? statuses,
       DateTime? startDate,
       DateTime? endDate,
       DateTime? confirmedFromDate,
       DateTime? confirmedToDate,
       string? filterData,
       bool? ownCompany,
       string[]? orderBy,
       long? companyId,
       int pageIndex,
       int pageSize,
       CT ct);

    Task<(List<RequestMachinery> Data, int RowCount)> GetOnProjectRequestReports(
          List<long>? ids,
          List<long>? costCenterIds,
          List<long>? projectIds,
          long? contractorId,
          List<long>? projectOperationIds,
          List<long>? projectOperationDetailIds,
          List<long>? machineryIds,
          RequestMachineryUnit? unit,
          RequestMachineryPaymentType? paymentType,
          DateTime? startDate,
          DateTime? endDate,
          DateTime? confirmedFromDate,
          DateTime? confirmedToDate,
          string? filterData,
          bool? ownCompany,
          string[]? orderBy,
          long? companyId,
          int pageIndex,
          int pageSize,
          CT ct);

    Task<(List<RequestMachinery> Data, int RowCount)> GetsMachineryRequesteExcelExporter(List<long>? ids,
        long? costCenterId,
        long? projectId,
        List<long>? projectOperationIds,
        long? machineriesGroupId,
        long? machineryId,
        RequestMachineryStatus? status,
        DateTime? fromDate,
        DateTime? toDate,
        DateTime? confirmedFromDate,
        DateTime? confirmedToDate,
        long? creatorId,
        long? operatorAppoinmentUserId,
        int? requestNumber,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<RequestMachinery?> GetByIdAsync(long requestMachineryId, CT ct);
    Task<GetRequestMachineryDriverModel?> GetRequestMachineryDriver(long? requestMachineryId, CT ct);
    Task<RequestMachinery?> GetByIdIncludeLess(long requestMachineryId, CT ct);
    Task<RequestMachinery?> GetByIdForUpdateMachineryAsync(long requestMachineryId, CT ct);
    Task<List<RequestMachinery>?> GetByIdsAsync(List<long> requestMachineryIds, CT ct);
    Task<List<RequestMachinery>?> GetByFixAssetId(long? FixAssetId, CT ct);
    Task<(List<long> Data, int RowCount)> GetsFilteredRequester(CT ct);
    Task<(List<long> Data, int RowCount)> GetsFilteredContractor(
        List<long>? costCenterIds,
        List<long>? projectIds,
        CT ct);
    Task<(List<long> Data, int RowCount)> GetOperationContractors(
        long? RequestMachineryId,
        CT ct);
    Task<bool> IsExistRequestMachinery(long projectId, long machineryId, decimal timeRequired, int requestCount, long? companyName, CT ct);

    Task<(List<RequestMachinery> Data, int RowCount)> GetFilteredForDailyAsync(long projectOperationDetailId, string? filterData, string[]? orderBy, int pageIndex, int pageSize, CT ct);

    Task<(List<RequestMachinery> Data, int RowCount)> GetTotalFiltered(
            List<long>? ids,
            long? costCenterId,
            long? projectId,
            List<long>? contractorIds,
            List<long>? projectOperationIds,
            List<long>? operationInfoIds,
            long? machineriesGroupId,
            long? machineryId,
            RequestMachineryStatus? status,
            RequestMachineryPaymentType? paymentType,
            DateTime? fromDate,
            DateTime? toDate,
            DateTime? confirmedFromDate,
            DateTime? confirmedToDate,
            long? creatorId,
            long? operatorAppoinmentUserId,
            int? requestNumber,
            string? driverName,
            string? filterData,
            long? companyId,
            string[]? orderBy,
            int pageIndex,
            int pageSize,
            CT ct);
}
