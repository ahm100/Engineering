using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCVEId;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails;

public class ProjectOperationDetailContractorExpertRepository : BaseRepository<EngineeringDBContext, ProjectOperationDetailContractorExpert>, IProjectOperationDetailContractorExpertRepository
{
    public ProjectOperationDetailContractorExpertRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectOperationDetailContractorExpert?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .Include(x => x.ProjectOperationDetailContractorService)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ProjectOperationDetailContractorExpert>?> GetByIds(
        List<long> ids, CT ct)
    {
        return await DbSet
            .Include(x => x.ProjectOperationDetailContractorService)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(ct);
    }

    public async Task<List<ProjectOperationDetailContractorExpert>?> GetByProjectOperationDetailContractorServiceId(
        long id, CT ct)
    {
        return await DbSet
            .Include(x => x.ProjectOperationDetailContractorService)
                .ThenInclude(x => x.ContractorContractDetailServices)
            .Where(x => x.ProjectOperationDetailContractorServiceId == id)
            .ToListAsync(ct);
    }

    public async Task<List<ProjectOperationDetailContractorExpert>?> GetByProjectOperationDetailContractorServiceIds(
        List<long> ids, CT ct)
    {
        return await DbSet
            .Include(x => x.ProjectOperationDetailContractorService)
                .ThenInclude(x => x.ContractorContractDetailServices)
            .Where(x => ids.Contains(x.ProjectOperationDetailContractorServiceId))
            .ToListAsync(ct);
    }

    public async Task<(List<GetPODContractorExpertsByCServiceIdModel>? Data, int RowCount)> GetPODContractorExpertsByCServiceId(
        long contractorServiceId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.ProjectOperationDetailContractorServiceId == contractorServiceId)
            .Select(x => new GetPODContractorExpertsByCServiceIdModel
            {
                Id = x.Id,
                Volume = x.Volume,
                HaveContract = x.HaveContract,
                ConsumableVolumeExpertId = x.ConsumableVolumeExpertId,
                SkillId = x.ConsumableVolumeExpert.ExpertId,
                ProjectOperationDetailContractorServiceId = x.ProjectOperationDetailContractorServiceId,
                ContractorId = x.ProjectOperationDetailContractorService.ContractorId,
                ContractorServiceStatus = x.ProjectOperationDetailContractorService.Status,
                Created = x.Created,
                CreatorId = x.CreatorId,
                Updated = x.Updated,
                UpdatorId = x.UpdaterId,
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }

    public async Task<(List<GetPODContractorExpertsByCVEIdModel>? Data, int RowCount)> GetPODContractorExpertsByCVEId(
        long consumableVolumeExpertId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.ConsumableVolumeExpertId == consumableVolumeExpertId)
            .Select(x => new GetPODContractorExpertsByCVEIdModel
            {
                Id = x.Id,
                Volume = x.Volume,
                HaveContract = x.HaveContract,
                ConsumableVolumeExpertId = x.ConsumableVolumeExpertId,
                SkillId = x.ConsumableVolumeExpert.ExpertId,
                ProjectOperationDetailContractorServiceId = x.ProjectOperationDetailContractorServiceId,
                ContractorId = x.ProjectOperationDetailContractorService.ContractorId,
                ContractorServiceStatus = x.ProjectOperationDetailContractorService.Status,
                Created = x.Created,
                CreatorId = x.CreatorId,
                Updated = x.Updated,
                UpdatorId = x.UpdaterId,
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }
}