using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;
using Engineering.Application.Services.CostCenterTypes.Models.GetsActiveCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Abstractions.Data.CostCenters;

public interface ICostCenterTypeRepository : IBaseRepository<CostCenterType>
{
    Task<CostCenterType?> GetCostCenterTypeByName(
        string name,
        long? companyId, CT ct);

    Task<GetCostCenterTypeByNameResponse?> GetCostCenterTypeByNameForResponse(
        string name,
        long? companyId, CT ct);

    Task<GetCostCenterTypeByIdResponse?> GetCostCenterTypeByIdForResponse(
        long id, CT ct);

    Task<CostCenterType?> GetCostCenterTypeById(
        long id, CT ct);

    Task<bool> GetCostCenterTypeByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long? companyId, CT ct);

    Task<string> CodeCreator(
        long? companyId, CT ct);

    Task<CostCenterType?> GetCostCenterTypeByCode(
        string code,
        long? companyId, CT ct);

    Task<GetCostCenterTypeByCodeResponse?> GetCostCenterTypeByCodeForResponse(
        string code,
        long? companyId, CT ct);

    Task<List<CostCenterType>> GetsCostCenterTypeByIds(
        List<long> ids, CT ct);

    Task<(List<GetsCostCenterTypeModel> Data, int RowCount)> GetsCostCenterType(
        List<long>? ids,
        string? filterData,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsActiveCostCenterTypesModel> Data, int RowCount)> GetsActiveCostCenterTypes(
        string? filterData,
        string? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct);
}