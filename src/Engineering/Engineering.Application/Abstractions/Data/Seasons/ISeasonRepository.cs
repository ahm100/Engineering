using Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;
using Engineering.Application.Services.Seasons.Models.GetSeasonByCode;
using Engineering.Application.Services.Seasons.Models.GetSeasonById;
using Engineering.Application.Services.Seasons.Models.GetSeasonByName;
using Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;
using Engineering.Application.Services.Seasons.Models.SeasonModels;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Abstractions.Data.Seasons;

public interface ISeasonRepository : IBaseRepository<Season>
{
    Task<Season?> FindByName(
        string name,
        long? companyId,
        long branchId,
        CT ct);

    Task<GetSeasonByNameResponse?> FindByNameForResponse(
        string name,
        long? companyId,
        long branchId,
        CT ct);

    Task<Season?> FindByCode(
        string code,
        long branchId,
        long? companyId, CT ct);

    Task<GetSeasonByCodeResponse?> FindByCodeForResponse(
        string code,
        long branchId,
        long? companyId, CT ct);

    Task<Season?> FindByIdWithBranch(
        long id, CT ct);

    Task<GetSeasonByIdResponse?> FindByIdWithBranchForResponse(
        long id, CT ct);

    Task<Season?> HaveSeasonChild(long Id, CT ct);
    Task<Season?> FindForDelete(
        long id, CT ct);

    Task<string> CodeCreator(
        long? companyId, CT ct);

    Task<bool> FindSeasonByNamesOrCodes(
        List<string> names,
        List<string> codes,
        long branchId,
        long? companyId, CT ct);

    Task<List<Season>> GetByCodes(
        List<string> catCodes,
        List<string> branchCodes,
        List<string> seasonCodes, CT ct);

    Task<List<Season>> GetByCodesIncludeNavigations(
      List<string> categoryCodes,
      List<string> branchCodes,
      List<string> seasonCodes,
      long companyId,
      CT ct);

    Task<(List<Season> Data, int RowCount)> GetSeasons(
        List<long>? ids,
        string? filterData,
        long? branchId,
        long? categoryId,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetSeasonsModel> Data, int RowCount)> GetSeasonsForResponse(
        List<long>? ids,
        string? filterData,
        long? branchId,
        long? categoryId,
        string? code,
        string? name,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<Season> Data, int RowCount)> GetActiveSeasons(
        string? filterData,
        long? branchId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);


    Task<(List<GetsActiveSeasonModel> Data, int RowCount)> GetActiveSeasonsForResponse(
        string? filterData,
        long? branchId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<Season> Data, int RowCount)> GetByBranchId(
        long branchId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsByBranchIdModel> Data, int RowCount)> GetByBranchIdForResponse(
        long branchId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<Season> Data, int RowCount)> GetBySeasonIds(
        List<long> seasonIds,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<Season> Data, int RowCount)> GetBySeasonIdsIncludeLess(
        List<long> seasonIds,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<Season> Data, int RowCount)> GetsByBranchIdWhithOperationInfo(
        long branchId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsByBranchIdWhithOperationInfoModel> Data, int RowCount)> GetsByBranchIdWhithOperationInfoForResponse(
        long branchId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<Season> Data, int RowCount)> GetsSeasonByBranchIds(
        List<long> branchIds,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetsSeasonByBranchIdsModel> Data, int RowCount)> GetsSeasonByBranchIdsForResponse(
        List<long> branchIds,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize, CT ct);

    Task<List<Season>> GetByNames(
        List<string> seasonName, CT ct);

    Task<List<Season>> GetByBranchId(
        long branchId,
        long companyId, CT ct);

    Task AddRangeAsync(IEnumerable<Season> seasons, CT ct);

    Task<List<Season>> GetForRasteReshteImport(List<long> branchIds, List<string> seasonCodes, long companyId, CT ct);
    Task<List<Season>> GetForFehrestBahaImport(
    long branchId,
    List<string> seasonCodes,
    long companyId,
    CT ct);
}