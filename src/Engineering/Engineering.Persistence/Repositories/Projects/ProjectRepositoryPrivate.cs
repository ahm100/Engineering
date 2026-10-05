using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Persistence.Repositories.Projects;

public partial class ProjectRepository : BaseRepository<EngineeringDBContext, Project>, IProjectRepository
{
    private IQueryable<GetProjectsModel> BuildQueryGetProjects(
        List<long>? ids,
        string? filterData,
        ProjectStatus? status,
        List<long>? categoryIds,
        List<long>? costCenterIds,
        long? advisorId,
        long? projectManagerId,
        long? planningAssistantId,
        long? thirdPartyId,
        long? supervisorEngineerId,
        long? implementationAssistantId,
        long? technicalAssistantId,
        long? employerId,
        long? projectTypeId,
        bool? isActive,
        long? companyId,
        List<ProjectStatus>? statuses,
        bool checkThirdParty,
        bool haveCostCenter,
        bool isOrganizationUnit)
    {
        var query = DbSet.AsQueryable();

        if (isOrganizationUnit)
            query = query.Where(x => x.IsOrganizationUnit == isOrganizationUnit);

        if (haveCostCenter)
            query = query.Where(x => x.ProjectCostCenters.Any());

        var newQuery = query
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                         (categoryIds == null || oo.ProjectCategories.Any(x => categoryIds.Contains(x.CategoryId))) &&
                         (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                         (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectCode!, filterData.MakeLikePattern()) ||
                          string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectName, filterData.MakeLikePattern())) &&
                         (costCenterIds == null || oo.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                         (advisorId == null || oo.Advisor == advisorId) &&
                         (status == null || oo.Status == status) &&
                         (projectManagerId == null || oo.ProjectManager == projectManagerId) &&
                         (planningAssistantId == null || oo.PlanningAssistant == planningAssistantId) &&
                         (supervisorEngineerId == null || oo.SupervisorEngineer == supervisorEngineerId) &&
                         (implementationAssistantId == null || oo.ProjectImplementationAssistants.Any(x => x.ImplementationAssistantUserId.Equals(implementationAssistantId))) &&
                         (technicalAssistantId == null || oo.ProjectTechnicalAssistants.Any(x => x.TechnicalAssistantUserId == technicalAssistantId)) &&
                         (employerId == null || oo.EmployerId == employerId) &&
                         (projectTypeId == null || oo.ProjectType!.Id == projectTypeId) &&
                         (statuses == null || statuses.Contains(oo.Status)) &&
                         (isActive == null || oo.IsActive == isActive) &&
                         (checkThirdParty == false || !oo.ProjectThirdParties.Any() || oo.ProjectThirdParties.Any(x => x.AuthorizedThirdPartyId == thirdPartyId)))
            .Select(x => new GetProjectsModel
            {
                Id = x.Id,
                AdvisorId = x.Advisor,
                Status = x.Status,
                ProjectManagerId = x.ProjectManager,
                CompanyId = x.CompanyId,
                Contractual = x.Contractual,
                CostCenterIds = x.ProjectCostCenters.Select(cc => cc.CostCenterId).ToList(),
                CostCenterNames = string.Join(", ", x.ProjectCostCenters.Select(cc => cc.CostCenter.CostCenterName)),
                CostCenterId = x.ProjectCostCenters.Any() ? x.ProjectCostCenters.FirstOrDefault()!.CostCenterId : null,
                CostCenterName = x.ProjectCostCenters.Any() ? x.ProjectCostCenters.FirstOrDefault()!.CostCenter!.CostCenterName : null,
                EmployerId = x.EmployerId,
                IsActive = x.IsActive,
                PlanningAssistantId = x.PlanningAssistant,
                ProjectCode = x.ProjectCode,
                Prefix = x.Prefix,
                ProjectName = x.ProjectName,
                ProjectEnName = x.ProjectEnName,
                ProjectTypeId = x.ProjectTypeId,
                ProjectTypeTitle = x.ProjectTypeId == null ? null : x.ProjectType!.ProjectTypeTitle,
                SupervisorEngineer = x.SupervisorEngineer,
                CityId = x.CityId,
                Description = x.Description,
                DescriptionEn = x.DescriptionEn,
                Created = x.Created,
                CreatorId = x.CreatorId,
                HasProduct = x.HasProduct,
                ApprovedBudget = x.ApprovedBudget,
                IsOrganizationUnit = x.IsOrganizationUnit,
                OrganizationId = x.OrganizationId,
                Category = x.ProjectCategories.Select(c => new GetProjectsCategoryModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.Category.CategoryName,
                    CategoryCode = c.Category.CategoryCode
                }).ToList()
            });

        return newQuery;
    }
}
