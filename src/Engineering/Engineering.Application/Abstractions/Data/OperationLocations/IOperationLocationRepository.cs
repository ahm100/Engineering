using Engineering.Application.Services.OperationLocations.Queries.GetsLocationByProjectOperationDetailIds;
using Engineering.Domain.Entities.OperationLocations;

namespace Engineering.Application.Abstractions.Data.OperationLocations;

public interface IOperationLocationRepository : IBaseRepository<OperationLocation>
{
    Task<OperationLocation?> GetOperationLocationById(
        long id, CT ct);

    Task<OperationLocation?> FindByName(
        string name,
        long? companyId,
        CT ct);

    Task<OperationLocation?> GetOperationLocationNameByCostCenter(
        string name,
        long? costCenterId,
        long? projectId,
        CT ct);

    Task<OperationLocation?> GetOperationLocationNameByParent(
        string name,
        long parentId,
        long? companyId,
        CT ct);

    Task<OperationLocation?> FindByCode(
        string code,
        long? companyId,
        CT ct);

    Task<List<OperationLocation>?> GetByCodes(
        List<string> codes,
        CT ct);

    Task<OperationLocation?> GetOperationLocationCodeByCostCenter(
        string code,
        long? costCenterId,
        long? projectId,
        CT ct);

    Task<OperationLocation?> GetOperationLocationCodeByParent(
        string code,
        long parentId,
        long? companyId,
        CT ct);

    Task<OperationLocation?> FindByIdWithCostCenter(
        long id,
        CT ct);

    Task<OperationLocation?> HaveOperationLocationChild(
        long id,
        CT ct);

    Task<string> CodeCreator(
        long? costCenterId,
        long? projectId,
        long? parentId,
        long? companyId,
        CT ct);

    Task<(List<OperationLocation> Data, int RowCount)> GetOperationLocations(
        List<long>? ids,
        long? costCenterId,
        long? projectId,
        long? parentId,
        string? filterData,
        bool? isActive,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationLocation> Data, int RowCount)> GetsOperationLocation(
        string? filterData,
        string? publicName,
        string? publicCode,
        List<long>? ids,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationLocation> Data, int RowCount)> GetsOperationLocationChild(
        long parentId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationLocation> Data, int RowCount)> GetActiveOperationLocations(
        string? filterData,
        long? costCenterId,
        long? projectId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationLocation> Data, int RowCount)> GetByCostCenterId(
        long? costCenterId,
        long? projectId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationLocation> Data, int RowCount)> GetsWithoutParentOperationLocation(
        long? costCenterId,
        long? projectId,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<OperationLocation> Data, int RowCount)> GetsByIds(
        List<long> ids,
        CT ct);

    Task<(List<GetsLocationByProjectOperationDetailIdsModel> Data, int RowCount)> GetsLocationByProjectOperationDetailIds(
        List<long> projectOperationDetailIds,
        CT ct);

    Task<(List<OperationLocation> Data, int RowCount)> GetByCostCenterAsync(
        long? costCenterId,
        long? projectId,
        string? filterData,
        List<long>? operationInfoIds,
        int pageIndex,
        int pageSize,
        CT ct);

}