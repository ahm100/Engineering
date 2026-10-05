using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;
using Engineering.Domain.Entities.Synonyms.Warehouse.WarehouseAssets;

namespace Engineering.Application.Abstractions.Data.Synonyms.Warhouse.WarehouseAssets;

public interface IViewWarehouseAssetRepository : IBaseRepository<ViewWarehouseAsset>
{
    Task<ViewWarehouseAsset?> GetById(
        long id, CT ct);

    Task<List<FilteredGroupsModel>?> GetFilteredGroupByCatAndWareIds(
        List<long> catIds,
        List<long>? wareIds,
        string? groupFilterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct);
}