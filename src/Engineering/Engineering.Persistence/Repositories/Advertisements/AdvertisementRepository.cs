using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementById;
using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementByIds;
using Engineering.Application.Services.Advertisements.Contracts.GetFltrAdvertisement;
using Engineering.Domain.Entities.Advertisements;

namespace Engineering.Persistence.Repositories.Advertisements;

public class AdvertisementRepository : BaseRepository<EngineeringDBContext, Advertisement>, IAdvertisementRepository
{
    public AdvertisementRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<Advertisement?> GetById(
        long id, CT ct)
    {
        var query = DbSet
            .Where(w =>
                w.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<List<Advertisement>?> GetByIds(
        List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(w =>
                ids.Contains(w.Id));

        var item = await query.ToListAsync(ct);
        return item;
    }

    public async Task<bool?> DoesTitleExist(
        long? id,
        string titleFa,
        string titleEn, CT ct)
    {
        return await DbSet
            .AnyAsync(w =>
                (id == null || w.Id != id) &&
                (w.TitleFa == titleFa ||
                w.TitleEn == titleEn), ct);
    }

    public async Task<GetAdvertisementByIdResponse?> GetAdvertisementById(
        long id, CT ct)
    {
        var query = DbSet
            .Where(w =>
                w.Id == id)
            .Select(x => new GetAdvertisementByIdResponse
            {
                Id = x.Id,
                TitleFa = x.TitleFa,
                TitleEn = x.TitleEn,
                DescriptionFa = x.DescriptionFa,
                DescriptionEn = x.DescriptionEn,
                TechnicalCode = x.TechnicalCode,
                Created = x.Created,
                CreatorId = x.CreatorId,
                IsActive = x.IsActive,
                DocumentUrls = x.AdvertisementDocuments.Select(x => x.Url).ToList()
            });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<(List<GetAdvertisementByIdsModel>? Data, int RowCount)> GetAdvertisementByIds(
        List<long> ids,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(w =>
                ids.Contains(w.Id))
            .Select(x => new GetAdvertisementByIdsModel
            {
                Id = x.Id,
                TitleFa = x.TitleFa,
                TitleEn = x.TitleEn,
                DescriptionFa = x.DescriptionFa,
                DescriptionEn = x.DescriptionEn,
                TechnicalCode = x.TechnicalCode,
                Created = x.Created,
                CreatorId = x.CreatorId,
                IsActive = x.IsActive,
                DocumentUrls = x.AdvertisementDocuments.Select(x => x.Url).ToList()
            });

        var count = await query.CountAsync();

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var item = await query.ToListAsync(ct);

        return (item, count);
    }

    public async Task<(List<GetFltrAdvertisementModel>? Data, int RowCount)> GetFltrAdvertisement(
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x =>
            ((isActive == null || x.IsActive == isActive) &&
            (string.IsNullOrWhiteSpace(filterData) ||
            EF.Functions.Like(x.TechnicalCode, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.TitleEn, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.TitleFa, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.DescriptionEn, filterData.MakeLikePattern()) ||
            EF.Functions.Like(x.DescriptionFa, filterData.MakeLikePattern()))))
            .Select(x => new GetFltrAdvertisementModel
            {
                Id = x.Id,
                TitleFa = x.TitleFa,
                TitleEn = x.TitleEn,
                DescriptionFa = x.DescriptionFa,
                DescriptionEn = x.DescriptionEn,
                TechnicalCode = x.TechnicalCode,
                Created = x.Created,
                CreatorId = x.CreatorId,
                IsActive = x.IsActive
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }
}