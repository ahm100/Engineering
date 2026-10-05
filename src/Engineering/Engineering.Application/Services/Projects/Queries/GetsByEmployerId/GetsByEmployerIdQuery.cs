using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsByEmployerId;

public record GetsByEmployerIdQuery(
    long EmployerId,
    string? FilterData,
    long? CompanyId,
    List<ProjectStatus>? Statuses,
    bool HaveCostCenter,
    bool IsOrganizationUnit,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Project>>>;