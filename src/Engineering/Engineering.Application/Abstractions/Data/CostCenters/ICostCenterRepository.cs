using Engineering.Application.Services.CostCenters.Models.CostCenterModels;
using Engineering.Application.Services.CostCenters.Models.GetCompaniesWork;
using Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Abstractions.Data.CostCenters;

public interface ICostCenterRepository : IBaseRepository<CostCenter>
{
    Task<CostCenter?> GetCostCenter(
        long id,
        CT ct);

    Task<CostCenter?> FindByName(
        string costCenterName,
        long? companyId,
        CT ct);

    Task<CostCenter?> FindByCode(
        string costCenterCode,
        long? companyId,
        CT ct);

    Task<CostCenter?> FindByIdAndChild(
        long id,
        CT ct);

    Task<CostCenter?> GetCostCenterWithoutInclude(
        long id,
        CT ct);

    Task<CostCenter?> GetCostCenterWithWarehousesInclude(
        long id,
        CT ct);

    Task<CostCenter?> GetCostCenterWithRolesInclude(
        long id,
        CT ct);

    Task<CostCenter?> GetCostCenterWithRolesAndUsersInclude(
        long id,
        CT ct);

    Task<CostCenter?> GetCostCenterWithUsersInclude(
        long id,
        CT ct);

    Task<CostCenter?> GetCostCenterWithInformedUsersInclude(
        long id,
        CT ct);

    Task<CostCenter?> GetForDelete(
        long id,
        CT ct);

    Task<string> CodeCreator(
        long? companyId,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetActiveCostCenters(
        string? filterData,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsActiveMainWarehouseCostCenterModel> Data, int RowCount)> GetsActiveMainWarehouseCostCenter(
        string? filterData,
        List<long>? warehouseIds,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetContractorCostCenters(
        long contractorId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsActiveAuthorizedCostCenter(
        string? filterData,
        long userId,
        long? employerId,
        long? costCenterTypeId,
        long? cityId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsAuthorizedCostCenter(
        string? filterData,
        long userId,
        long? employerId,
        long? costCenterTypeId,
        long? cityId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetCostCentersModel> Data, int RowCount)> GetCostCenters(
        List<long>? ids,
        string? filterData,
        string? costCenterName,
        string? costCenterCode,
        long? costCenterTypeId,
        long? informedUserId,
        long? authorizedRoleId,
        long? authorizedUserId,
        long? warehouseId,
        long? cityId,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsByNameOrCode(
        string filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsCostCenterByWarehouse(
        long warehouseId,
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsByAuthorizedUserId(
        string? filterData,
        long userId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsByAuthorizedRoleId(
        string? filterData,
        long roleId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsByEmployerId(
        string? filterData,
        long employerId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsCostCenterByContractorId(
        string? filterData,
        long contractorId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsCostCenterByProjectManagerId(
        string? filterData,
        long projectManagerId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsByCityId(
        long cityId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsByTypeId(
        long typeId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<CostCenter> Data, int RowCount)> GetsCostCenterByIds(
        List<long>? ids,
        List<Guid>? preferentialReferenceCodes,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<long>?> GetsCostCenterWarehouseByProjectOperation(
        long projectOperationId,
        CT ct);

    Task<List<long>?> GetFilteredCostCenterCities(
        List<long>? costCenterIds,
        CT ct);

    Task<List<CostCenter>?> GetByCodes(
        List<string> costCenterCodes,
        long? companyId,
        CT ct);

    Task<List<CostCenter>?> GetByIdsIncludeType(
        List<long> ids,
        CT ct);

    Task<CostCenter?> GetIsDefaultCostCenterByCompanyId(
        long companyId,
        CT ct);

    Task<(List<GetCompaniesCostCentersModel> Data, int RowCount)> GetCompaniesWork(
            long companyId, CT ct);
}