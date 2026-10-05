using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.WarehouseAssets;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;
using Engineering.Domain.Entities.Synonyms.Warehouse.WarehouseAssets;

namespace Engineering.Persistence.Repositories.Synonyms.Warehouse.WarehouseAssets;

public class ViewWarehouseAssetRepository : BaseRepository<EngineeringDBContext, ViewWarehouseAsset>, IViewWarehouseAssetRepository
{
    public ViewWarehouseAssetRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewWarehouseAsset?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<FilteredGroupsModel>?> GetFilteredGroupByCatAndWareIds(
        List<long> catIds,
        List<long>? wareIds,
        string? groupFilterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x => catIds.Contains(x.Group.Category.Id) &&
            (wareIds == null || wareIds.Contains(x.WarehouseId)) &&
            (string.IsNullOrWhiteSpace(groupFilterData) || EF.Functions.Like(x.Group.Code, groupFilterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(groupFilterData) || EF.Functions.Like(x.Group.Name, groupFilterData.MakeLikePattern())) &&

            (string.IsNullOrWhiteSpace(productFilterData) || x.Group.Products.Any(z => EF.Functions.Like(z.Name, productFilterData.MakeLikePattern())) ||
            string.IsNullOrWhiteSpace(productFilterData) || x.Group.Products.Any(z => EF.Functions.Like(z.Code, productFilterData.MakeLikePattern()))))

            .Select(x => new FilteredGroupsModel(
                x.GroupId,
                x.Group.Name,
                x.Group.CommercialName,
                x.Group.Code,
                x.Group.DocumentGroups.Select(x => x.Document.FileUrl).ToList(),
                x.Group.Description,
                x.Group.BarCode,
                x.Group.IranCode,
                x.Group.QrCode,
                x.Group.CategoryId,
                x.Group.Category.Code,
                x.Group.MinimumTemperature,
                x.Group.MaximumTemperature,
                x.Group.MeasureUnitId,
                x.Group.MeasureUnit.Name,
                x.Group.IsSaleable,
                x.Group.IsPresentable,
                null,
                x.Group.IsPerishable,
                x.Group.IsActive,
                null,
                x.Group.BrandId,
                x.Group.BrandModelId,
                x.Group.Products
                .Select(p => new GroupProductsModel(
                    p.Id,
                    p.Name,
                    p.Code,
                    p.Brand.Name,
                    p.BrandModel.Name,
                    p.IsActive
                    ))
                .ToList()
                ));

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return await query.ToListAsync(ct);

    }
}