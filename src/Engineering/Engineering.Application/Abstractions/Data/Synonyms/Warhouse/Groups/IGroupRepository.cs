using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;

namespace Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Groups;

public interface IViewGroupRepository : IBaseRepository<ViewGroup>
{
    Task<ViewGroup?> GetById(
        long id, CT ct);

    Task<long?> GetGroupCategoryId(
        long id, CT ct);

    Task<List<ViewGroup>> GetByIds(
        List<long> ids, CT ct);

    Task<List<Group>> GetGroupsByIds(
        List<long> ids, CT ct);

    Task<List<GetGroupWithCategoryId>> GetGroupCategoryByProductIds(
        List<long> groupIds, CT ct);

    Task<List<FilteredGroup>> GetFilteredGroupsByIds(
        List<long> ids,
        string? filterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ViewGroup>> GetFilteredGroupsByCategoryIds(
        List<long> categoryIds,
        string? filterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<FilteredGroupsModel>?> GetFilteredGroupByCatIds(
        List<long> catIds,
        string? groupFilterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct);
}