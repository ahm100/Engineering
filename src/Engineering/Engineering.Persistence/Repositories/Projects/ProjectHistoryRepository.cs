using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Models.GetProjectHistory;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Domain.Entities.Projects.Histories;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectHistoryRepository : BaseRepository<EngineeringDBContext, ProjectHistory>, IProjectHistoryRepository
{
    public ProjectHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<GetProjectHistoryModel>?> GetProjectHistory(
        long id,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.ProjectId == id)

            .Select(x => new GetProjectHistoryModel
            {
                Id = x.Id,
                ProjectTypeId = x.Project.ProjectTypeId,
                ProjectTypeTitle = x.Project.ProjectType.ProjectTypeTitle,
                ProjectName = x.ProjectName,
                ProjectCode = x.ProjectCode,
                Prefix = x.Prefix,
                CostCenterId = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterName = x.Project.ProjectCostCenters.Any() ? x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                SupervisorEngineer = x.SupervisorEngineer,
                AdvisorId = x.Advisor,
                ProjectManagerId = x.ProjectManager,
                PlanningAssistantId = x.PlanningAssistant,
                EmployerId = x.EmployerId,
                Status = x.Status,
                Contractual = x.Contractual,
                CollectiveService = x.CollectiveService,
                PreferentialReferenceCode = x.PreferentialReferenceCode,
                CompanyId = x.CompanyId,
                Category = x.Project.ProjectCategories.Select(x => new GetProjectsCategoryModel
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    CategoryCode = x.Category.CategoryCode
                }).ToList()
            });

        var result = await query.ToListAsync();
        return result;
    }
}
