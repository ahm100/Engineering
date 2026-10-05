using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrProducts;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Products.Models;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class ProductRepository : BaseRepository<EngineeringDBContext, ViewProduct>, IViewProductRepository
{
    public ProductRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewProduct?> GetProductById(
        long id,
        CT ct)
    {
        var query = await DbSet
            .Include(x => x.Brand)
            .Include(x => x.BrandModel)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted && !x.Group.IsDeleted, ct);

        return query;
    }

    public async Task<List<ViewProduct>?> GetProductByGroupIds(
        List<long> ids,
        string? productFilterData,
        CT ct)
    {
        var query = await DbSet
            .Where(x => ids.Contains(x.GroupId) &&
            (string.IsNullOrWhiteSpace(productFilterData) || EF.Functions.Like(x.Code, productFilterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(productFilterData) || EF.Functions.Like(x.Name, productFilterData.MakeLikePattern())))
            .ToListAsync(ct);

        return query;
    }

    public async Task<List<ViewProduct>?> GetProductByCodes(
        List<string> codes,
        CT ct)
    {
        var query = await DbSet
            .Include(x => x.Group)
                .ThenInclude(x => x.Category)
            .Where(x => x.IsActive && !x.IsDeleted && codes.Contains(x.Code))
            .ToListAsync(ct);

        return query;
    }

    public async Task<(List<ViewProduct> Data, int RowCount)> GetProductsByIds(
        List<long> ids,
        bool? ignoreQuery,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.Brand)
            .Include(x => x.BrandModel)
            .Include(x => x.Group)
            .ThenInclude(x => x.MeasureUnit)
            .Where(x =>
                ids.Contains(x.Id) &&
                (filterData == null || EF.Functions.Like(x.Name, filterData.MakeLikePattern())
                                    || EF.Functions.Like(x.Code, filterData.MakeLikePattern())));

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var count = ignoreQuery == true
            ? await query.IgnoreQueryFilters().CountAsync(ct)
            : await query.CountAsync(ct);

        var items = ignoreQuery == true
            ? await query.IgnoreQueryFilters().ToListAsync(ct)
            : await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<GetGoodsSupplyDetailProductsModelDetail>?> GetProductForDetailsModel(
    List<long> ids,
    string? productFilterData,
    CT ct)
    {
        var query = await DbSet
            .Where(x =>
                ids.Contains(x.GroupId) &&
                (string.IsNullOrWhiteSpace(productFilterData) ||
                 EF.Functions.Like(x.Code, productFilterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.Name, productFilterData.MakeLikePattern()))
            )
            .Select(x => new GetGoodsSupplyDetailProductsModelDetail
            {
                Id = x.Id,
                GroupId = x.GroupId,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,

                Measure = x.Group.MeasureUnit.Name,

                BrandId = x.BrandId,
                Brand = x.Brand.Name,

                BrandModelId = x.BrandModelId,
                BrandModel = x.BrandModel.Name,

                IsActive = x.IsActive,

                Urls = x.Group.DocumentGroups
                    .Select(d => d.Document.FileUrl)
                    .ToList()
            })
            .ToListAsync(ct);

        return query;
    }
    public async Task<List<GetGoodsSupplyDetailProductsModelDetail>?> GetProductForPOProductsDetailsModel(
    List<long> ids,
    string? productFilterData,
    CT ct)
    {
        var query = await DbSet
            .Where(x =>
                ids.Contains(x.GroupId) &&
                (string.IsNullOrWhiteSpace(productFilterData) ||
                 EF.Functions.Like(x.Code, productFilterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.Name, productFilterData.MakeLikePattern()))
            )
            .Select(x => new GetGoodsSupplyDetailProductsModelDetail
            {
                Id = x.Id,
                GroupId = x.GroupId,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                Measure = x.Group.MeasureUnit.Name,
                BrandId = x.BrandId,
                Brand = x.Brand.Name,
                BrandModelId = x.BrandModelId,
                BrandModel = x.BrandModel.Name,
                IsActive = x.IsActive,
                Urls = x.Group.DocumentGroups
                    .Select(d => d.Document.FileUrl)
                    .ToList()
            })
        .ToListAsync(ct);

        return query;
    }

    public async Task<List<GetProductModel>?> GetProductByIds(
        List<long> ids,
        CT ct)
    {
        var query = await DbSet
            .Where(x => ids.Contains(x.Id))
            .Select(x => new GetProductModel
            {
                Id = x.Id,
                Brand = x.Brand.Name,
                BrandId = x.BrandId,
                BrandModel = x.BrandModel.Name,
                BrandModelId = x.BrandModelId,
                ProductDescription = x.Description,
                IsActive = x.IsActive,
                Features = null,
                Name = x.Name,
                NameEn = x.NameEn,
                Code = x.Code,

                Group = new GroupResponse
                {
                    Id = x.Group.Id,
                    Name = x.Group.Name,
                    Code = x.Group.Code,
                    IsSaleable = x.Group.IsSaleable,
                    HasSerialNo = false,
                    IsPerishable = x.Group.IsPerishable,
                    IsActive = x.Group.IsActive,
                    Measure = x.Group.MeasureUnit.Name,
                    MeasureId = x.Group.MeasureUnitId,
                    Category = new CategoryResponse(
                        x.Group.CategoryId,
                        x.Group.Category.Title,
                        x.Group.Category.Code,
                        x.Group.Category.IsActive)
                }
            })
            .ToListAsync(ct);

        return query;
    }

    public async Task<List<Product>?> GetFltrProductByIds(
        List<long> ids,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x => ids.Contains(x.Id) &&
                (EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.Name, filterData.MakeLikePattern())))
            .Select(x => new Product
            {
                Id = x.Id,
                Brand = x.Brand.Name,
                BrandId = x.BrandId,
                BrandModel = x.BrandModel.Name,
                BrandModelId = x.BrandModelId,
                IsActive = x.IsActive,
                Features = null,
                Name = x.Name,
                Code = x.Code,

                Group = new GroupResponse
                {
                    Id = x.Group.Id,
                    Name = x.Group.Name,
                    Code = x.Group.Code,
                    IsSaleable = x.Group.IsSaleable,
                    HasSerialNo = false,
                    IsPerishable = x.Group.IsPerishable,
                    IsActive = x.Group.IsActive,
                    Measure = x.Group.MeasureUnit.Name,
                    MeasureId = x.Group.MeasureUnitId,
                    Category = new CategoryResponse(
                        x.Group.CategoryId,
                        x.Group.Category.Title,
                        x.Group.Category.Code,
                        x.Group.Category.IsActive)
                }
            });

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return baseQuery;
    }

    public async Task<(List<GetFltrProductsModel>? Data, int RowCount)> GetFltrProducts(
        List<long>? catIds,
        List<long>? groupIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x =>
                (catIds == null || catIds.Count < 1 || catIds.Contains(x.Group.CategoryId)) &&
                (groupIds == null || groupIds.Count < 1 || groupIds.Contains(x.GroupId)) &&
                (string.IsNullOrWhiteSpace(filterData) ||
                EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
                 EF.Functions.Like(x.Name, filterData.MakeLikePattern())))
            .Select(x => new GetFltrProductsModel
            {
                Id = x.Id,
                Name = x.Name,
                TechnicalCode = x.TechnicalCode,
                GroupId = x.GroupId,
                GroupName = x.Group.Name
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }

    public async Task<(List<GetFltrProductsModel>? Data, int RowCount)> GetFltrProducts(
        string? faFilterData,
        string? enFilterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(x =>
                (string.IsNullOrWhiteSpace(faFilterData) ||
                 EF.Functions.Like(x.Name, faFilterData.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(enFilterData) ||
                 EF.Functions.Like(x.NameEn, enFilterData.MakeLikePattern())))
            .Select(x => new GetFltrProductsModel
            {
                Id = x.Id,
                Name = x.Name,
                TechnicalCode = x.TechnicalCode,
                GroupId = x.GroupId,
                GroupName = x.Group.Name
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }
}
