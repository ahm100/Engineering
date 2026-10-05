using Engineering.Application.Abstractions.Data.Adjustments;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexById;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexes;
using Engineering.Domain.Entities.Adjustments;

namespace Engineering.Persistence.Repositories.Adjustments;

public class AdjustmentIndexRepository
    : BaseRepository<EngineeringDBContext, AdjustmentIndex>,
      IAdjustmentIndexRepository
{
    public AdjustmentIndexRepository(
        EngineeringDBContext context)
        : base(context)
    {
    }

    public async Task AddRangeAsync(IEnumerable<AdjustmentIndex> adjustmentIndexes, CT ct)
    {
        await DbSet.AddRangeAsync(adjustmentIndexes, ct);
    }

    public async Task<AdjustmentIndex?> GetById(long id, CT ct)
    {
        return await DbSet
            .Include(x => x.Values)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<GetAdjustmentIndexByIdResponse?> GetByIdForApi(long id, CT ct)
    {
        return await DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetAdjustmentIndexByIdResponse()
            {
                Id = x.Id,
                AdjustmentReferenceId = x.AdjustmentReferenceId,
                AdjustmentReferenceTitle = x.AdjustmentReference.Title,
                YearId = x.YearId,
                BranchId = x.BranchId,
                BranchTitle = x.Branch.BranchName,
                SeasonId = x.SeasonId,
                SeasonTitle = x.Season != null ? x.Season.SeasonName : null,
                Code = x.Code,
                Title = x.Title,
                Description = x.Description,
                DocumentFile = x.DocumentFile,
                IsActive = x.IsActive,
                Values = x.Values
                    .Select(v => new GetAdjustmentIndexValueModel
                    {
                        Id = v.Id,
                        YearName = v.YearName,
                        Period = v.Period,
                        Type = v.Type,
                        Value = v.Value,
                        Coefficient = v.Coefficient,
                        NotificationNumber = v.NotificationNumber,
                        NotificationDate = v.NotificationDate,
                        IsActive = v.IsActive
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<GetAdjustmentIndexesResponse> GetAdjustmentIndexes(GetAdjustmentIndexesRequest request, CT ct)
    {
        var pageIndex = request.PageIndex ?? 1;
        var pageSize = request.PageSize ?? 20;

        var query = DbSet
            .Where(x =>
                (request.AdjustmentReferenceId == null ||
                 x.AdjustmentReferenceId == request.AdjustmentReferenceId) &&
                (request.BranchId == null ||
                 x.BranchId == request.BranchId) &&
                (request.SeasonId == null ||
                 x.SeasonId == request.SeasonId) &&
                (string.IsNullOrWhiteSpace(request.Search) ||
                 x.Code.Contains(request.Search) ||
                 x.Title.Contains(request.Search)))
            .OrderByDescending(x => x.Created)
             .Select(x => new GetAdjustmentIndexesModel
             {
                 Id = x.Id,
                 Code = x.Code,
                 Title = x.Title,
                 AdjustmentReferenceTitle = x.AdjustmentReference.Title,
                 BranchId = x.BranchId,
                 BranchTitle = x.Branch.BranchName,
                 SeasonId = x.SeasonId,
                 SeasonTitle = x.SeasonId.HasValue
                ? x.Season!.SeasonName
                : null,
                 IsActive = x.IsActive
             });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query.ToListAsync(ct);
        return new GetAdjustmentIndexesResponse(data, count);
    }

    public async Task<bool> ExistsByReferenceAndCode(long adjustmentReferenceId,
        string code, long? excludeId, CT ct)
    {
        return await DbSet.AnyAsync(x =>
                x.AdjustmentReferenceId == adjustmentReferenceId &&
                x.Code == code && (excludeId == null || x.Id != excludeId), ct);
    }
}