using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails.ConsumableVolume;

public class ConsumableVolumeExpertRepository : BaseRepository<EngineeringDBContext, ConsumableVolumeExpert>, IConsumableVolumeExpertRepository
{
    public ConsumableVolumeExpertRepository(EngineeringDBContext context) : base(context) { }

    public async Task<ConsumableVolumeExpert?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperationDetail)
                .ThenInclude(x => x.ProjectOperation)
                    .ThenInclude(x => x.OperationInfo)
                        .ThenInclude(x => x.ConsumptionStandardExperts)
            .Where(oo => oo.Id == id && !oo.ProjectOperationDetail.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<ConsumableVolumeExpert> Data, int RowCount)> GetsByProjectOperationDetailId(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperationDetail)
            .Where(oo => oo.ProjectOperationDetail!.Id == id && oo.IsDeleted != true);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ExpertsDataModel> Data, int RowCount)> GetsByProjectOperationId(long projectOperationId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ProjectOperationDetail!.ProjectOperation.Id == projectOperationId && oo.IsDeleted != true)
            .Include(oo => oo.ProjectOperationDetail)
                .ThenInclude(oo => oo.ConsumableVolumeExperts)
            .Select(e => new ExpertsDataModel
            {
                Id = e.Id,
                ProjectOperationDetailId = e.ProjectOperationDetail.Id,
                ExpertId = e.ExpertId,
                Number = e.Number,
                UnusedPercentage = e.UnusedPercentage,
                IsStandard = e.IsStandard,
                StandardValue = e.StandardValue,
                FinalValue = e.FinalValue
            });

        var count = await query.CountAsync(ct);
        var items = await query.Page(pageIndex, pageSize).ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ConsumableVolumeExpert>> GetExpertsByProjectOperationIds(List<long> projectOperationIds, CT ct)
    {
        var query = DbSet
            .Where(oo => projectOperationIds.Contains(oo.ProjectOperationDetail!.ProjectOperation.Id))
            .Include(oo => oo.ProjectOperationDetail)
                .ThenInclude(oo => oo.ConsumableVolumeExperts);

        var items = await query.ToListAsync(ct);
        return (items);
    }

}