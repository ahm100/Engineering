using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsProjectByIds;

public record GetsProjectByIdsQuery(
    List<long>? Ids,
    string? FilterData,
    List<ProjectStatus>? Statuses,
    bool HaveCostCenter,
    bool IsOrganizationUnit,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Project>>>;
