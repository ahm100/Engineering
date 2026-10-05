using Engineering.Application.Services.Branchs.Models.GetBranchByCode;
using Engineering.Application.Services.Branchs.Models.GetBranchByName;
using Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;
using Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;
using Engineering.Application.Services.Branchs.Models.GetsBranchs;
using Engineering.Application.Services.Branchs.Models.GetsByCategoryId;
using Engineering.Domain.Entities.Branchs;

namespace Engineering.Application.Abstractions.Data.Branchs;

public interface IBranchRepository : IBaseRepository<Branch>
{
    Task<GetBranchByNameResponse?> GetBranchByName(
        string name,
        long categoryId,
        long? companyId, CT ct);

    Task<bool> GetBranchByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long categoryId,
        long? companyId, CT ct);

    Task<GetBranchByCodeResponse?> GetBranchByCode(
        string code,
        long categoryId,
        long? companyId, CT ct);

    Task<Branch?> GetBranchByIdWithCategory(
        long id, CT ct);

    Task<Branch?> HaveBranchChild(
        long id, CT ct);

    Task<Branch?> GetBranchWithoutInclude(
        long id, CT ct);

    Task<string> CodeCreator(
        long? companyId, CT ct);

    Task<List<Branch>> GetsBranchByIds(
        List<long> ids, CT ct);

    Task<(List<Branch> Data, int RowCount)> GetsBranchByCodes(
        List<string> codes,
        long categoryId,
        long? companyId, CT ct);

    Task<(List<Branch> Data, int RowCount)> GetsBranchs(
        List<long>? ids,
        string? filterData,
        long? categoryId,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsBranchsResponseModel> Data, int RowCount)> GetsBranchsResponse(
        string? filterData,
        long? categoryId,
        string? code,
        string? name,
        bool? isActive,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsActiveBranchsResponseModel> Data, int RowCount)> GetsActiveBranchs(
        string? filterData,
        long? categoryId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<Branch> Data, int RowCount)> GetsBranchByCategoryId(
        long categoryId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsBranchByCategoryIdModel> Data, int RowCount)> GetsBranchByCategoryIdModel(
        long categoryId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsBranchByCategoryIdsModel> Data, int RowCount)> GetsBranchByCategoryIds(
        List<long> categoryIds,
        string? filterData,
        bool? isActive,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct);

    Task AddRangeAsync(IEnumerable<Branch> branches, CT ct);
    Task<List<Branch>> GetForRasteReshteImport(List<long> categoryIds, List<string> branchCodes, long companyId, CT ct);
    Task<List<Branch>> GetForAdjustmentImport(List<string> categoryCodes, List<string> branchCodes, long companyId, CT ct);

}