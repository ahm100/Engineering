using Engineering.Application.Services.CostOvers.Models.GetCostOverByCode;
using Engineering.Application.Services.CostOvers.Models.GetCostOverById;
using Engineering.Application.Services.CostOvers.Models.GetCostOverByName;
using Engineering.Application.Services.CostOvers.Models.GetsActiveCostOvers;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverByNameOrCode;
using Engineering.Application.Services.CostOvers.Models.GetsCostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Abstractions.Data.CostOvers;

public interface ICostOverRepository : IBaseRepository<CostOver>
{
    Task<CostOver?> GetCostOverByName(
        string name,
        long? companyId, CT ct);

    Task<GetCostOverByNameResponse?> GetCostOverByNameForResponse(
        string costOverName,
        long? companyId, CT ct);

    Task<CostOver?> GetCostOverById(
       long id, CT ct);

    Task<GetCostOverByIdResponse?> GetCostOverByIdForResponse(
        long id, CT ct);

    Task<CostOver?> GetCostOverByCode(
        string code,
        long? companyId, CT ct);

    Task<GetCostOverByCodeResponse?> GetCostOverByCodeForResponse(
        string costOverCode,
        long? companyId, CT ct);

    Task<string> CodeCreator(
        long? companyId, CT ct);

    Task<bool> GetsCostOverByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long? companyId, CT ct);

    Task<List<CostOver>> GetsCostOverByIds(
        List<long> ids, CT ct);

    Task<(List<GetsCostOversModel> Data, int RowCount)> GetsCostOvers(
        List<long>? ids,
        string? filterData,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<CostOver> Data, int RowCount)> GetsCostOversByIds(
        List<long> ids, CT ct);

    Task<(List<GetsCostOverByNameOrCodeModel> Data, int RowCount)> GetsCostOverByNameOrCode(
        string filterData,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<CostOver> Data, int RowCount)> GetsByNameOrCode
        (string filterData,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsActiveCostOversModel> Data, int RowCount)> GetsActiveCostOvers(
        string? filterData,
        string? code,
        string? name,
        int pageIndex,
        int pageSize, CT ct);
}