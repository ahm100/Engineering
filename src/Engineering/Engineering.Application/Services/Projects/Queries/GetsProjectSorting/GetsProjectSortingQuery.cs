using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsProjectSorting;

public record GetsProjectSortingQuery(
    string? FilterData,
    ProjectStatus? Status,
    bool? IsActive,
    long? CompanyId,
    bool HaveCostCenter,
    bool IsOrganizationUnit,
    List<ProjectStatus>? Statuses
    ) : IQuery<DataResult<IQueryable<Project>>>;
