
using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectsByCostCenter;

public record GetProjectsByCostCenterQuery(
    long CostCenterId,
    string? FilterData,
    List<ProjectStatus>? Statuses,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Project>>>;