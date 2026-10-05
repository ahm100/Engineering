using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Groups;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Groups.Models;

namespace Engineering.Persistence.Repositories.Synonyms.Warehouse.Groups;

public class ViewGroupRepository : BaseRepository<EngineeringDBContext, ViewGroup>, IViewGroupRepository
{
    public ViewGroupRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewGroup?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<long?> GetGroupCategoryId(
        long id, CT ct)
    {
        return await DbSet.Where(x => x.Id == id).Select(x => x.CategoryId)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<ViewGroup>> GetByIds(
        List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }

    public async Task<List<Group>> GetGroupsByIds(
        List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(x => ids.Contains(x.Id) && !x.IsDeleted && !x.Category.IsDeleted)
            .Select(x => new Group(x.Id,
            x.Name,
            x.CommercialName,
            x.Code,
            x.Description,
            x.BarCode,
            x.IranCode,
            x.QrCode,
            x.MinimumTemperature,
            x.MaximumTemperature,
            x.MeasureUnitId,
            x.MeasureUnit.Name,
            x.IsSaleable,
            x.IsPresentable,
            false,
            x.IsPerishable,
            x.IsActive));

        return await query.ToListAsync(ct);
    }

    public async Task<List<GetGroupWithCategoryId>> GetGroupCategoryByProductIds(
        List<long> groupIds, CT ct)
    {
        return await DbSet
            .Where(x => groupIds.Contains(x.Id) && !x.IsDeleted && !x.Category.IsDeleted)
            .Select(x => new GetGroupWithCategoryId
            {
                GroupId = x.Id,
                CategoryId = x.CategoryId,
            }).ToListAsync(ct);
    }

    public async Task<List<FilteredGroup>> GetFilteredGroupsByIds(
        List<long> ids,
        string? filterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => ids.Contains(x.Id) &&

        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Name, filterData.MakeLikePattern())) &&

        (string.IsNullOrWhiteSpace(productFilterData) || x.Products.Any(z => EF.Functions.Like(z.Name, productFilterData.MakeLikePattern())) ||
            string.IsNullOrWhiteSpace(productFilterData) || x.Products.Any(z => EF.Functions.Like(z.Code, productFilterData.MakeLikePattern()))))

        .Select(x => new FilteredGroup(
            x.Id,
            x.Name,
            x.CommercialName,
            x.Code,
            x.DocumentGroups.Select(x => x.Document.FileUrl).ToList(),
            x.Description,
            x.IsActive,
            x.MeasureUnitId,
            x.MeasureUnit.Name
            ));

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return await query.ToListAsync(ct);
    }

    public async Task<List<FilteredGroupsModel>?> GetFilteredGroupByCatIds(
        List<long> catIds,
        string? groupFilterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x => catIds.Contains(x.CategoryId) &&
            (string.IsNullOrWhiteSpace(groupFilterData) || EF.Functions.Like(x.Code, groupFilterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(groupFilterData) || EF.Functions.Like(x.Name, groupFilterData.MakeLikePattern())) &&

            (string.IsNullOrWhiteSpace(productFilterData) || x.Products.Any(z => EF.Functions.Like(z.Name, productFilterData.MakeLikePattern())) ||
            string.IsNullOrWhiteSpace(productFilterData) || x.Products.Any(z => EF.Functions.Like(z.Code, productFilterData.MakeLikePattern()))))

            .Select(x => new FilteredGroupsModel(
                x.Id,
                x.Name,
                x.CommercialName,
                x.Code,
                x.DocumentGroups.Select(x => x.Document.FileUrl).ToList(),
                x.Description,
                x.BarCode,
                x.IranCode,
                x.QrCode,
                x.CategoryId,
                x.Category.Code,
                x.MinimumTemperature,
                x.MaximumTemperature,
                x.MeasureUnitId,
                x.MeasureUnit.Name,
                x.IsSaleable,
                x.IsPresentable,
                null,
                x.IsPerishable,
                x.IsActive,
                null,
                x.BrandId,
                x.BrandModelId,
                x.Products
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

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return await query.ToListAsync(ct);

    }


    public async Task<List<ViewGroup>> GetFilteredGroupsByCategoryIds(
        List<long> categoryIds,
        string? filterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => categoryIds.Contains(x.CategoryId) &&

        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(x.Name, filterData.MakeLikePattern())) &&

        (string.IsNullOrWhiteSpace(productFilterData) || x.Products.Any(z => EF.Functions.Like(z.Name, productFilterData.MakeLikePattern())) ||
            string.IsNullOrWhiteSpace(productFilterData) || x.Products.Any(z => EF.Functions.Like(z.Code, productFilterData.MakeLikePattern()))));

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return await query.ToListAsync(ct);
    }
}