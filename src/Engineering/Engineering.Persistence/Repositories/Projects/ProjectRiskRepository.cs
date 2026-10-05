using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectRisks.Contracts.GetFltrProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskByProjectId;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectRiskRepository : BaseRepository<EngineeringDBContext, ProjectRisk>, IProjectRiskRepository
{
    public ProjectRiskRepository(EngineeringDBContext context) : base(context)
    {

    }

    public async Task<ProjectRisk?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<(List<GetProjectRiskByProjectIdModel>? Data, int RowCount)> GetProjectRiskByProjectId(
        long id,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x => x.ProjectId == id)
            .Select(x => new GetProjectRiskByProjectIdModel
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Title,
                ProjectId = x.ProjectId,
                ProjectName = x.Project.ProjectName,
                RiskProbability = x.RiskProbability,
                RiskImpact = x.RiskImpact,
                RiskStatus = x.RiskStatus,
                CreatorId = x.CreatorId,
                Created = x.Created
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }

    public async Task<GetProjectRiskByIdResponse?> GetProjectRiskById(
        long id, CT ct)
    {
        return await DbSet
            .Where(x => x.Id == id)
            .Select(x => new GetProjectRiskByIdResponse
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Title,
                ProjectId = x.ProjectId,
                ProjectName = x.Project.ProjectName,
                RiskProbability = x.RiskProbability,
                RiskImpact = x.RiskImpact,
                RiskStatus = x.RiskStatus,
                CreatorId = x.CreatorId,
                Created = x.Created
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetFltrProjectRiskModel>? Data, int RowCount)> GetFltrProjectRisk(
        List<long>? projectIds,
        string? filterData,
        RiskProbability? riskProbability,
        RiskImpact? riskImpact,
        RiskStatus? riskStatus,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x =>
        (projectIds == null || projectIds.Contains(x.ProjectId)) &&
        (riskProbability == null || x.RiskProbability == riskProbability) &&
        (riskImpact == null || x.RiskImpact == riskImpact) &&
        (riskStatus == null || x.RiskStatus == riskStatus) &&
        (string.IsNullOrWhiteSpace(filterData) ||
        (EF.Functions.Like(x.Code, filterData.MakeLikePattern()) ||
        EF.Functions.Like(x.Title, filterData.MakeLikePattern()))))
            .Select(x => new GetFltrProjectRiskModel
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Title,
                ProjectId = x.ProjectId,
                ProjectName = x.Project.ProjectName,
                RiskProbability = x.RiskProbability,
                RiskImpact = x.RiskImpact,
                RiskStatus = x.RiskStatus,
                CreatorId = x.CreatorId,
                Created = x.Created
            });

        var count = await query.CountAsync();
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }
}