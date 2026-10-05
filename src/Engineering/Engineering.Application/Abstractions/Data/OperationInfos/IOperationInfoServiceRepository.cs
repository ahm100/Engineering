using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceByProjectId;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Abstractions.Data.OperationInfos;

public interface IOperationInfoServiceRepository : IBaseRepository<OperationInfoService>
{
    Task<(List<OperationInfoService> Data, int RowCount)> GetsByOprationInfoId(
        long oprationInfoId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfoService> Data, int RowCount)> GetsByOperationInfoIdIncludeless(
        long oprationInfoId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<OperationInfoService?> GetOperationInfoServiceByIdForDelete(
        long oprationInfoId,
        long serviceInfoId,
        CT ct);

    Task<bool> ValidateForContractorServices(
        long oprationInfoId,
        long serviceInfoId,
        CT ct);

    Task<OperationInfoService?> GetOperationInfoServiceForValidation(
        long oprationInfoId,
        long serviceInfoId,
        CT ct);

    Task<(List<OperationInfoService> Data, int RowCount)> GetsOperationInfoServiceFiltered(
        long? categoryId,
        long? branchId,
        long? seasonId,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfoService> Data, int RowCount)> GetsOperationInfoServiceByIds(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<OperationInfoService>> GetsOperationInfoServiceByOIId(
        long id,
        List<long> serviceIds,
        CT ct);

    Task<(List<GetsOperationInfoServiceByProjectIdModel> Data, int RowCount)> GetsOperationInfoServiceByProjectId(
        long projectId,
        long? excludedServiceInfoId,
        string? serviceInfoFilters,
        string? operationInfoFilters,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

}