using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfosByCodes;
using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredForReports;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Abstractions.Data.OperationInfos;

public interface IOperationInfoRepository : IBaseRepository<OperationInfo>
{
    Task<OperationInfo?> GetOperationInfoByIdIncludeLess(
        long id, CT ct);

    Task<OperationInfo?> GetOperationInfoByIdWithChild(
        long id, CT ct);

    Task<OperationInfo?> GetOperationInfoByIdByConsumptionStandards(
        long id, CT ct);

    Task<OperationInfo?> GetOperationInfoByIdByExperts(
        long id, CT ct);

    Task<OperationInfo?> GetOperationInfoByIdByMachineries(
        long id, CT ct);

    Task<OperationInfo?> GetOperationInfoByIdByProducts(
        long id, CT ct);

    Task<OperationInfo?> GetOperationInfoByIdByGroupRelations(
        long id, CT ct);

    Task<OperationInfo?> GetOperationInfoByIdByProjectOperations(
        long id, CT ct);

    Task<bool> GetOperationInfoProjectOperationsValidator(
        long projdctId,
        long operationInfoId,
        long? employerContractId,
        long unitOfMeasurementId, CT ct);

    Task<OperationInfo?> GetByIdWithChild(
        long id, CT ct);

    Task<OperationInfo?> GetOperationInfoWithProjectOperationId(
        long id, CT ct);

    Task<OperationInfo?> GetByIdWithDependencies(
        long id, CT ct);

    Task<OperationInfo?> GetOperationInfo(
        long id, CT ct);

    Task<OperationInfo?> FindForDelete(
        long id, CT ct);

    Task<OperationInfo?> HaveOperationInfoChild(
        long id, CT ct);

    Task<OperationInfo?> FindByName(
        string name, long? unitOfMeasurementId, long? companyId, CT ct);

    Task<OperationInfo?> FindByCode(
        string code, long? companyId, CT ct);

    Task<List<OperationInfo>?> GetByCodes(
        List<string> codes, CT ct);

    Task<string> CodeCreator(
        long? companyId, CT ct);

    Task<List<OperationInfo>> GetOperationInfos(
        List<long> ids, CT ct);

    Task<(List<GetOperationInfosModel> Data, int RowCount)> GetFilteredOperationInfo(
        List<long>? ids,
        string? filterData,
        long? categoryId,
        long? branchId,
        long? seasonId,
        bool? isActive,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetsByMultiFilter(
        string filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<long> Data, int RowCount)> GetOperationInfoContractors(CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetsBySeasonId(
        long seasonId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetsOperationInfoByContractorIds(
        List<long>? contractorIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetsByIds(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetsOperationInfoByIdsForSeason(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetsOperationInfoByIdsForServiceInfo(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetOperationInfosModel> Data, int RowCount)> GetOperationInfosModelByIds(
        List<long> ids,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetsOperationInfoByIdsIncludeLess(
        List<long> ids, CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetsConsiderationOperationInfos(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetActiveOperationInfos(
        string? filterData,
        long? categoryId,
        long? branchId,
        long? seasonId,
        int? priority,
        long? company,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetsPrioritizeOperationInfo(
        string? filterData,
        long? id,
        int priority,
        long? company,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationInfo> Data, int RowCount)> GetByCostCenterAsync(
        long costCenterId,
        long projectId,
        string? filterData,
        List<long>? operationLocationIds,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsFilteredForReportsModel> Data, int RowCount)> GetsFilteredForReports(
        List<long>? costCenterIds,
        List<long>? projectIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<OperationInfo>> GetActiveOperationInfoBySeasonIds(
        List<long>? categoryIds,
        List<long>? branchIds,
        List<long>? seasonIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<OperationInfoBulkDto>> FindByCodes(
        List<string> codes, long? companyId, CT ct);

    Task AddRangeAsync(IEnumerable<OperationInfo> operationInfos, CT ct);

}