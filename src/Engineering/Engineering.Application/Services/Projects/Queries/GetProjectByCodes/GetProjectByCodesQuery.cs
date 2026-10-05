using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByCodes;

public record GetProjectByCodesQuery(
    List<string> ProjectCodes,
    List<ProjectStatus>? Statuses,
    bool HaveCostCenter,
    bool IsOrganizationUnit,
    long? CompanyId
    ) : IQuery<List<Project>?>;